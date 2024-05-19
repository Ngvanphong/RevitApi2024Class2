using Autodesk.Revit.UI;
using RevitApi2024.CreateBeamFrom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace RevitApi2024.CreateColunn
{
    public class CreateColumnHandler : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            UIDocument uiDoc = app.ActiveUIDocument;
            Document doc = uiDoc.Document;

            XYZ pointColumn = null;
            try
            {
                pointColumn = uiDoc.Selection.PickPoint();
            }
            catch { };

            FamilySymbol familySymbol = CreateColumnAppShow.frmCreateColumn.comboboxTypeColumn.SelectedItem as FamilySymbol;
            using (Transaction tr = new Transaction(doc, "CreateColunn"))

            {
                tr.Start();
                if (!familySymbol.IsActive) { familySymbol.Activate(); }
                doc.Create.NewFamilyInstance(pointColumn, familySymbol, doc.ActiveView.GenLevel, Autodesk.Revit.DB.Structure.StructuralType.Column );
                tr.Commit();
            }

        }

        public string GetName()
        {
            return "columnHandler";
        }
    }
}
