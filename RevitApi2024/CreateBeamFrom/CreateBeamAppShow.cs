using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitApi2024.CreateBeamFrom
{
    public static class CreateBeamAppShow
    {
        public static wpfCreateBeam frmCreateBeam;
        public static XYZ startPoint;
        public static XYZ endPoint; 
        public static void ShowForm()
        {
            try
            {
                frmCreateBeam.Close();
            }
            catch { }

            PickPointHandler pickPointHandler= new PickPointHandler();
            ExternalEvent pickPointEvent= ExternalEvent.Create(pickPointHandler);

            CreateBeamHandler createBeamHandler= new CreateBeamHandler(); 
            ExternalEvent createBeamEvent = ExternalEvent.Create(createBeamHandler);

            frmCreateBeam = new wpfCreateBeam(pickPointEvent, createBeamEvent);
            frmCreateBeam.Show();
        }
    }
}
