using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using RevitApi2024.Properties;

namespace RevitApi2024.Button
{
    internal class CreateBeamButton
    {
        public void CreateBeam(UIControlledApplication applicaton)
        {
            try
            {
                 applicaton.CreateRibbonTab(AppConstant.RibbonName);
            }
            catch { }
     
            RibbonPanel panel = applicaton.GetRibbonPanels(AppConstant.RibbonName).FirstOrDefault(x=>x.Name==AppConstant.PanelName);
            if(panel ==null)
                panel= applicaton.CreateRibbonPanel(AppConstant.RibbonName, AppConstant.PanelName);


            PushButtonData pushButtonData = new PushButtonData("CreateBeam", "Create \n Beam",
                Assembly.GetExecutingAssembly().Location, "RevitApi2024.CreateBeam.CreateBeamBinding");
            BitmapSource imageResource = Extension.GetImageSource(Resources.icons8_crop_24);
            pushButtonData.Image = imageResource;
            pushButtonData.LargeImage = imageResource;
            pushButtonData.ToolTip = "Create Beam in Revit";
            pushButtonData.LongDescription = "Create Beam log Description";
            (panel.AddItem(pushButtonData) as PushButton).Enabled= true;

        }
    }
}
