using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitApi2024.CreateBeamFrom
{
    public class PickPointHandler : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            UIDocument uiDoc = app.ActiveUIDocument;
            Document doc= uiDoc.Document;
            try
            {
                CreateBeamAppShow.startPoint = uiDoc.Selection.PickPoint("Pick start point");
                CreateBeamAppShow.endPoint = uiDoc.Selection.PickPoint("Pick end point");
            }
            catch { return; }
            CreateBeamAppShow.frmCreateBeam.IsEnableOkButton = true;
        }

        public string GetName()
        {
            return "PickPointHandler23";
        }
    }
}
