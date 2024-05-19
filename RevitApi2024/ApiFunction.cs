using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Import thư viện của revit để tương tác với revit API
using Autodesk.Revit.DB; // lưu trữ tất cả các dữ liệu của mô hình 
using Autodesk.Revit.UI; // quan lý việc chọn , pick đối tượng , tương tác chuột

using Autodesk.Revit.DB.Architecture; // tường tác các đôi tượng kiến trúc như tường , room ,..
using Autodesk.Revit.DB.Structure; // tường tác vói các đối tượng kết cấu như rebar;

using Autodesk.Revit.DB.Mechanical; // tương tác với các thư viện duct pile;
using Autodesk.Revit.DB.Electrical; // tương tác với các thư viện Electrical;
using Autodesk.Revit.DB.Plumbing;  // tương tác vói các thư viên pumbing;

using Autodesk.Revit.UI.Selection;
using System.IO;
using Autodesk.Revit.ApplicationServices;




namespace RevitApi2024
{
    public class ApiFunction
    {
        public void FunctionRevit()
        {
            UIDocument uiDoc = null;
            Document doc = uiDoc.Document;

            try
            {
                // Chon một điểm trên mặt bằng
                XYZ pickPoint = uiDoc.Selection.PickPoint("Pick một điểm trên mặt bằng");

                // Pick một đối tượng trong revit
                var pickElement = uiDoc.Selection.PickObject(ObjectType.Element, "Chọn một đối tượng");

                // Pick đối tường theo điều kiện mong muốn.
                var pickeElement2 = uiDoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element,
                    new WallFilter(), "Chọn đối tượng wall");

                Wall element = doc.GetElement(pickeElement2) as Wall;

                // Pick nhiều đối tượng
                var pickElements = uiDoc.Selection.PickObjects(ObjectType.Element, "Chọn nhiều đối tượng");

                var pickBox = uiDoc.Selection.PickBox(PickBoxStyle.Crossing, "Chọn đối tượng theo box");
                XYZ min = pickBox.Min;
                XYZ max = pickBox.Max;
                
                // pick đôi tượng trên mặt bằng
                var pickRectange = uiDoc.Selection.PickElementsByRectangle();


            }
            catch { }

            
            XYZ min1= null;
            XYZ max1 = null;
            Outline outline = new Outline(min1, max1);
            // chỉ filter những đối tượng bị cắt qua hình chủ nhật có hai góc là min, max
            BoundingBoxIntersectsFilter boundingBoxFilter = new BoundingBoxIntersectsFilter(outline);
            
            // lọc đối tượng trong revit
            var collection = new FilteredElementCollector(doc, doc.ActiveView.Id)
                .OfCategory(BuiltInCategory.OST_Walls)
                .WhereElementIsNotElementType()
                .WhereElementIsElementType()
                .OfClass(typeof(WallType))
                .WherePasses(boundingBoxFilter) // loc theo filter của revit
                .Cast<WallType>()
                .Where(x => x.Name == "Wall1")
                .FirstOrDefault();

            using(TransactionGroup tg= new TransactionGroup(doc, "GroupElement"))
            {
                tg.Start();

                using(Transaction t1= new Transaction(doc, "trans1"))
                {
                    t1.Start();

                    t1.Commit();
                }
                using (Transaction t2 = new Transaction(doc, "trans1"))
                {
                    t2.Start();

                    t2.Commit();
                }


                tg.Assimilate();
            }

            UIApplication app=null;
            CategorySet categorySet = new CategorySet();
            Categories allCategory= doc.Settings.Categories;
            Category categoryWall = null;
            foreach(Category item in allCategory)
            {
                if(item.Id.Value == (long)BuiltInCategory.OST_Walls)
                {
                    categoryWall = item;
                    break;
                }

            }

            categoryWall= Autodesk.Revit.DB.Category.GetCategory(doc,BuiltInCategory.OST_Walls);

            categorySet.Insert(categoryWall);
            RawCreateProjectParameter(app, "Prameter 1", SpecTypeId.String.Text, true, categorySet, GroupTypeId.Data,true);







        }

        public static void RawCreateProjectParameter(UIApplication app, string name, ForgeTypeId type, bool visible, CategorySet cats, ForgeTypeId group, bool inst)
        {
            //InternalDefinition def = new InternalDefinition();
            //Definition def = new Definition();

            string oriFile = app.Application.SharedParametersFilename;
            string tempFile = Path.GetTempFileName() + ".txt";
            using (File.Create(tempFile)) { }
            app.Application.SharedParametersFilename = tempFile;

            var defOptions = new ExternalDefinitionCreationOptions(name, type)
            {
                Visible = visible
            };
            ExternalDefinition def = app.Application.OpenSharedParameterFile().Groups.Create("VolumeDefintionGroup").Definitions.Create(defOptions) as ExternalDefinition;

            app.Application.SharedParametersFilename = oriFile;
            File.Delete(tempFile);

            Autodesk.Revit.DB.Binding binding = app.Application.Create.NewTypeBinding(cats);
            if (inst) binding = app.Application.Create.NewInstanceBinding(cats);

            BindingMap map = (new UIApplication(app.Application)).ActiveUIDocument.Document.ParameterBindings;
            map.Insert(def, binding, group);
        }


    }

    public class WallFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            return elem.Category != null && elem.Category.Id.Value == (long)BuiltInCategory.OST_Walls;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return true;
        }
    }
}
