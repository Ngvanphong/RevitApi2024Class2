﻿using Autodesk.Revit.UI;
using RevitApi2024.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RevitApi2024.Button
{
    public class CreateColumnButton
    {
        public void CreateColumn(UIControlledApplication applicaton)
        {
            try
            {
                applicaton.CreateRibbonTab(AppConstant.RibbonName);
            }
            catch { }

            RibbonPanel panel = applicaton.GetRibbonPanels(AppConstant.RibbonName).FirstOrDefault(x => x.Name == AppConstant.PanelName);
            if (panel == null)
                panel = applicaton.CreateRibbonPanel(AppConstant.RibbonName, AppConstant.PanelName);


            PushButtonData pushButtonData = new PushButtonData("CreateColumn", "Create \n Column",
                Assembly.GetExecutingAssembly().Location, "RevitApi2024.CreateColunn.CreateColumnBinding");
            BitmapSource imageResource = Extension.GetImageSource(Resources.icons8_crop_24);
            pushButtonData.Image = imageResource;
            pushButtonData.LargeImage = imageResource;
            pushButtonData.ToolTip = "Create Column in Revit";
            pushButtonData.LongDescription = "Create Column log Description";
            (panel.AddItem(pushButtonData) as PushButton).Enabled = true;

        }
    }
}
