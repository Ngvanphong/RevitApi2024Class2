using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using netDxf.Collections;
using Newtonsoft.Json;
using RevitApi2024.CreateBeamFrom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RevitApi2024
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            //var collection = new FilteredElementCollector(doc)
            //    .OfCategory(BuiltInCategory.OST_StructuralColumns)
            //    .WhereElementIsNotElementType()
            //    .OfClass(typeof(FamilyInstance)).Cast<FamilyInstance>();
            //List<Data> listData= new List<Data>();
            ////foreach (FamilyInstance fa in collection)
            ////{
            ////    if (!(fa.Location is LocationPoint)) continue;
            ////    XYZ locaiton = (fa.Location as LocationPoint).Point;
            ////    Point point = new Point(locaiton.X, locaiton.Y, locaiton.Z);
            ////    Information information = new Information(point, "Material");
            ////    Data data = new Data(fa.Id.Value, fa.Name, information);
            ////    listData.Add(data);
            ////}

            //string textJson = JsonConvert.SerializeObject(listData);
            //string folderDocument = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //string path = Path.Combine(folderDocument, "data.json");
            ////File.WriteAllText(path, textJson);

            //string textFromFile = File.ReadAllText(path);
            //listData = JsonConvert.DeserializeObject<List<Data>>(textFromFile);

            //Transform cadTransform = null;
            //var newPoint = cadTransform.OfPoint(new XYZ(1, 2, 2));


            var viewSetCollection= new FilteredElementCollector(doc).OfClass(typeof(ViewSheetSet))
                .Cast<ViewSheetSet>();

            PrintManager printManager = doc.PrintManager;
            ViewSheetSetting viewSheetSetting = printManager.ViewSheetSetting ;
            ViewSheetSet viewSheetSet = viewSetCollection.First(x => x.Name == "11");
            viewSheetSetting.CurrentViewSheetSet= viewSheetSet;

            //ViewSet myViewSet = new ViewSet();
            //foreach (var sheet in viewSheetSet.OrderedViewList)
            //{
            //    myViewSet.Insert(sheet);
            //}

            //viewSheetSetting.CurrentViewSheetSet.Views = myViewSet;



            printManager.PrintRange = PrintRange.Select;
            printManager.PrintToFile = true;
            string pathDoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string project = doc.ProjectInformation.Name;
            var listString= project.Split('-');
            string fileName = string.Empty;
            for(int i=0; i<listString.Length; i++)
            {
                if (i == 2)
                {

                    fileName += "-" + viewSheetSet.Name+"-";
                }
                fileName+= listString[i];
            }
            string path= Path.Combine(pathDoc, fileName);

            var interator = printManager.PaperSizes.ForwardIterator();
            interator.MoveNext();

            IPrintSetting printSettings = printManager.PrintSetup.CurrentPrintSetting;
            printSettings.PrintParameters.PageOrientation = PageOrientationType.Landscape;
            printSettings.PrintParameters.PaperSize =  interator.Current as PaperSize;
            printSettings.PrintParameters.ZoomType = ZoomType.Zoom;
            printSettings.PrintParameters.Zoom = 100;
            printSettings.PrintParameters.ColorDepth = ColorDepthType.GrayScale;

            printManager.PrintSetup.CurrentPrintSetting = printSettings;

            printManager.CombinedFile = true;
            printManager.Apply();

            //foreach(Autodesk.Revit.DB.View sheet in myViewSet)
            //{

            //    printManager.SubmitPrint(sheet);
            //}



            return Result.Succeeded;
        }

        public void GetSolidOfGeomInstance(ref List<Solid> listResult, GeometryInstance instance)
        {
            var geonInst= instance.GetInstanceGeometry();
            List<Solid> solidResut= new List<Solid>();
            foreach(GeometryObject geoObj in geonInst)
            {
                if(geoObj is Solid)
                {
                    Solid solid= geoObj as Solid;
                    if(solid.Volume> 0.0001)
                    {
                        solidResut.Add(solid);
                    }
                    
                }else if(geoObj is GeometryInstance)
                {
                    GetSolidOfGeomInstance(ref listResult,geoObj as GeometryInstance);
                }
            }
            if(solidResut.Count>0) listResult.AddRange(solidResut);
            return;
        }





        public List<Solid> GetSolidOfGeomInstance( GeometryInstance instance)
        {
            var geonInst = instance.GetInstanceGeometry();
            List<Solid> solidResut = new List<Solid>(); 
            foreach (GeometryObject geoObj in geonInst)
            {
                if (geoObj is Solid)
                {
                    Solid solid = geoObj as Solid;
                    if (solid.Volume > 0.0001)
                    {
                        solidResut.Add(solid);
                    }

                }
                else if (geoObj is GeometryInstance)
                {
                   solidResut.AddRange(GetSolidOfGeomInstance( geoObj as GeometryInstance)); 
                }
            }
            return solidResut;
        }

        public void GetIntOfGeomInstance(ref List<int> listResult, List<TestClass> testClass)
        {
            foreach (TestClass item in testClass)
            {
                listResult.Add(item.Solid);
                GetIntOfGeomInstance(ref listResult, item.Items);
            }
            return;
        }

        public List<int> GetIntSolid(List<TestClass> testClass)
        {
            List<int> resultInt = new List<int>();
            foreach(TestClass item in testClass)
            {
                resultInt.Add(item.Solid);
                GetIntSolid(item.Items);
            }
            return resultInt;
        }


    }

}


public class TestClass
{
    public TestClass()
    {
        Items = new List<TestClass>();
    }
    public int Solid { set; get; }

    public List<TestClass> Items { set; get; }
}
