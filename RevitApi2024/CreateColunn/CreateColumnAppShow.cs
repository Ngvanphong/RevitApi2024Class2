using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitApi2024.CreateBeamFrom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitApi2024.CreateColunn
{
    public static class CreateColumnAppShow
    {
        public static wpfCreateColumn frmCreateColumn;
        public static XYZ Point;

        public static void ShowForm()
        {
            try
            {
                frmCreateColumn.Close();
               
            }
            catch { }


            CreateColumnHandler createColumnHandler = new CreateColumnHandler();
            ExternalEvent createColumnEvent = ExternalEvent.Create(createColumnHandler);

            frmCreateColumn = new wpfCreateColumn(createColumnEvent);
            frmCreateColumn.Show();
            
        }
    }
}
