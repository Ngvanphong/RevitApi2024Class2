using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitApi2024.CreateBeamFrom
{
    [Transaction(TransactionMode.Manual)]
    public class CreateBeam2Binding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            var pickBox = uiDoc.Selection.PickBox(PickBoxStyle.Crossing, "Chọn đối tượng theo box");

            IEnumerable<FamilySymbol> collection = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_StructuralFraming)
                .WhereElementIsElementType().OfClass(typeof(FamilySymbol)).Cast<FamilySymbol>();
            collection= collection.OrderBy(x=>x.Name);

            //var form = new wpfCreateBeam();
            //form.comboboxTypeBeam.ItemsSource = collection;
            //form.ShowDialog();
            //if (form.DialogResult == true)
            //{
            //    TaskDialog.Show("CreateBeam", "Addin create beam");
            //}

            CreateBeamAppShow.ShowForm();
            CreateBeamAppShow.frmCreateBeam.comboboxTypeBeam.ItemsSource = collection;


            return Result.Succeeded;
        }
    }
}
