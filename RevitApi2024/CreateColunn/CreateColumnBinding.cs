using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace RevitApi2024.CreateColunn
{
    [Transaction(TransactionMode.Manual)]
    public class CreateColumnBinding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            var collection = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_StructuralColumns).WhereElementIsElementType().OfClass(typeof(FamilySymbol));


            CreateColumnAppShow.ShowForm();
            CreateColumnAppShow.frmCreateColumn.comboboxTypeColumn.ItemsSource = collection;
            
            return Result.Succeeded;
        }

    }
}
