using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using excel = Microsoft.Office.Interop.Excel;
using netDxf;
using netDxf.Entities;

namespace RevitApi2024.CreateBeamFrom
{
    [Transaction(TransactionMode.Manual)]
    public class CreateBeam2Binding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            ImportInstance cadImport = doc.GetElement(uiDoc.Selection.GetElementIds().First()) as ImportInstance;

            Options options = new Options();
            options.View = doc.ActiveView;
            options.IncludeNonVisibleObjects = true;
            options.ComputeReferences = true;
            GeometryElement geoElement = cadImport.get_Geometry(options);
            List<Autodesk.Revit.DB.Line> listLine = new List<Autodesk.Revit.DB.Line>();
            List<PolyLine> listPolyLine = new List<PolyLine>();
            Transform cadTransform = null;
            foreach (GeometryObject geoObj in geoElement)
            {
                GeometryInstance geoIns= geoObj as GeometryInstance;
                if(geoIns != null)
                {
                    cadTransform= geoIns.Transform;
                    GeometryElement geoSubElement = geoIns.GetInstanceGeometry();
                    foreach(GeometryObject geoSubObj in geoSubElement)
                    {
                        if(geoSubObj is Autodesk.Revit.DB.Line line)
                        {
                            GraphicsStyle graphicStyle = doc.GetElement(geoSubObj.GraphicsStyleId) as GraphicsStyle;
                            if(graphicStyle != null)
                            {
                                if (graphicStyle.GraphicsStyleCategory.Name == "0" || graphicStyle.GraphicsStyleCategory.Name=="Revit")
                                {
                                    listLine.Add(line);
                                }
                            }


                        }
                        else if(geoSubObj is PolyLine polyline)
                        {
                            GraphicsStyle graphicStyle = doc.GetElement(geoSubObj.GraphicsStyleId) as GraphicsStyle;
                            if (graphicStyle != null)
                            {
                                if (graphicStyle.GraphicsStyleCategory.Name == "0" || graphicStyle.GraphicsStyleCategory.Name == "Revit")
                                {
                                    listPolyLine.Add(polyline);
                                }
                            }
                        }
                    }
                }
            }

            string pathDxf = ExportDxf(doc, cadImport);
            if (!string.IsNullOrEmpty(pathDxf))
            {
                List<TextFromCad> listTextData = GetTextFromCad(pathDxf, cadTransform);
                try
                {
                    System.IO.File.Delete(pathDxf);
                }
                catch { }
               
            }

           return Result.Succeeded;
        }

        public string ExportDxf(Document doc, ImportInstance cadImport)
        {
            string folder = Path.GetTempPath();
            string fileName = doc.ProjectInformation.Name + Guid.NewGuid() + ".dxf";
            bool isSucessed = false;
            using(Transaction t= new Transaction(doc, "ExportDxf"))
            {
                t.Start();
                var hideCollection= new FilteredElementCollector(doc,doc.ActiveView.Id).
                    WhereElementIsNotElementType().ToElements()
                    .Where(x=>x.CanBeHidden(doc.ActiveView)&& x.Id!=cadImport.Id)
                    .ToList();
                if(hideCollection.Count>0)
                {
                    doc.ActiveView.HideElements(hideCollection.Select(x => x.Id).ToList());
                }
                doc.Regenerate();

                DXFExportOptions dxfOption = new DXFExportOptions();
                dxfOption.FileVersion = ACADVersion.R2013;
                isSucessed= doc.Export(folder, fileName, new List<ElementId> { doc.ActiveView.Id},dxfOption);
                t.Commit();
            }
            if (!isSucessed) return string.Empty;
            return Path.Combine(folder, fileName);
        }

        public List<TextFromCad> GetTextFromCad(string path,Transform transform)
        {
            List<TextFromCad> listDataText = new List<TextFromCad>();
            DxfDocument dxfDoc= DxfDocument.Load(path);
            double mmToInch = 1 / 304.8;
            foreach(var block in dxfDoc.Blocks)
            {
                foreach(var entity in block.Entities)
                {
                    if(entity is Text || entity is MText)
                    {
                        if(entity is Text)
                        {
                            string value = (entity as Text).Value;
                            Vector3 position = (entity as Text).Position;
                            XYZ xyz = new XYZ(position.X * mmToInch, position.Y * mmToInch, 0);
                            xyz= transform.OfPoint(xyz);
                            string layerName = entity.Layer.Name;
                            TextFromCad textData= new TextFromCad(value,layerName, xyz);
                            listDataText.Add(textData);
                        }else if(entity is MText)
                        {
                            string value= (entity as MText).Value;
                            Vector3 position = (entity as MText).Position;
                            XYZ xyz = new XYZ(position.X * mmToInch, position.Y* mmToInch, 0);
                            xyz = transform.OfPoint(xyz);
                            string layerName = entity.Layer.Name;
                            TextFromCad textData = new TextFromCad(value, layerName, xyz);
                            listDataText.Add(textData);
                        }
                    }
                }
            }
            return listDataText;
        }


    }

    public class TextFromCad
    {
        public string Value { set; get; }
        public string Layer { set; get; }

        public XYZ Position { set; get; }

        public TextFromCad(string value, string layer, XYZ position)
        {
            (Value,Layer,Position)= (value,layer,position);
        }
    }
}
