using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitApi2024.CreateBeamFrom
{
    public class CreateBeamHandler : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            UIDocument uiDoc = app.ActiveUIDocument;
            Document doc = uiDoc.Document;
            Line line = Line.CreateBound(CreateBeamAppShow.startPoint, CreateBeamAppShow.endPoint);
            FamilySymbol familySymbol= CreateBeamAppShow.frmCreateBeam.comboboxTypeBeam.SelectedItem as FamilySymbol;
            using(Transaction tr= new Transaction(doc, "CreateBeam"))
            {
                tr.Start();
                if(!familySymbol.IsActive) { familySymbol.Activate(); }
                doc.Create.NewFamilyInstance(line, familySymbol, doc.ActiveView.GenLevel, 
                    Autodesk.Revit.DB.Structure.StructuralType.Beam);
                tr.Commit();
            }

        }

        public string GetName()
        {
            return "CreateBeamHandler";
        }
    }
}
