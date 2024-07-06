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

            using(Transaction tr= new Transaction(doc, "CreateBeam"))
            {
                tr.Start();
               
                tr.Commit();
            }

        }

        public string GetName()
        {
            return "CreateBeamHandler";
        }
    }
}
