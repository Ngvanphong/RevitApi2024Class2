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

           

        }

        public string GetName()
        {
            return "columnHandler";
        }
    }
}
