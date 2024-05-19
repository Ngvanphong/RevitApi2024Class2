using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
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
            Element element = null;
            element = doc.GetElement(uiDoc.Selection.GetElementIds().First());

            Options geomOption = new Options();
            geomOption.IncludeNonVisibleObjects = false;
            //geomOption.View = doc.ActiveView; // lay che do fine cross, medium cua view hien tai

            geomOption.DetailLevel = ViewDetailLevel.Fine; // chi dinh che do hinh hoc;
            geomOption.ComputeReferences = true; // dung de dim kich thuoc

            GeometryElement geoElement = element.get_Geometry(geomOption);
            foreach (GeometryObject geoObj in geoElement)
            {
                if (geoObj is Solid)
                {
                    Solid solid = geoObj as Solid;
                    //double m3 = UnitUtils.ConvertFromInternalUnits(solid.Volume, UnitTypeId.CubicMeters);
                    if (solid.Volume > 0.000001)
                    {
                        var faces = solid.Faces;
                        foreach (Face face in faces)
                        {
                            IList<CurveLoop> curveloopsOfFace = face.GetEdgesAsCurveLoops();
                            var edgeOfFace = face.EdgeLoops;
                        }
                    }

                }
                else if (geoObj is Curve)
                {

                }
                else if (geoObj is GeometryInstance)
                {

                }
            }

            List<TestClass> testClasses = new List<TestClass>();
            TestClass t1 = new TestClass();
            t1.Solid = 1;

            var t11= new TestClass();
            t11.Solid = 2;

            var t12 = new TestClass();
            t12.Solid = 3;

            var t111= new TestClass();
            t111.Solid = 4;

            t1.Items= new List<TestClass>();    
            t1.Items.Add(t11);
            t1.Items.Add(t12);

            t11.Items=new List<TestClass>();
            t11.Items.Add(t111);

            testClasses.Add(t1);

            List<int> resultInt = GetIntSolid(testClasses);

            List<int> result2 = new List<int>();
            GetIntOfGeomInstance(ref result2, testClasses);

            RevitLinkInstance revitRevit = doc.GetElement(uiDoc.Selection.GetElementIds().First()) as RevitLinkInstance;

            Document documentLink = revitRevit.GetLinkDocument();
            var beamCollection = new FilteredElementCollector(documentLink).OfCategory(BuiltInCategory.OST_StructuralFraming);
            




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
