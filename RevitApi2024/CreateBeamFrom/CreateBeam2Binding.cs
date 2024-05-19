using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using excel = Microsoft.Office.Interop.Excel;

namespace RevitApi2024.CreateBeamFrom
{
    [Transaction(TransactionMode.Manual)]
    public class CreateBeam2Binding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            Document doc = uiDoc.Document;

            OpenFileDialog openFileDialog= new OpenFileDialog();
            openFileDialog.Title = "Browser Excel Files";
            openFileDialog.DefaultExt = "xlsx";
            openFileDialog.Filter = "excel files (*.xlsx)|*.xlsx";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<BeamProperty> properties = new List<BeamProperty>();
                string path = openFileDialog.FileName;
                excel.Application appExecel = new excel.Application();
                excel.Workbook workbook= appExecel.Workbooks.Open(path);
                excel.Worksheet worksheet = workbook.Worksheets[1];
                excel.Range useRange = worksheet.UsedRange;
                int countRow = useRange.Rows.Count;
                for(int row = 2; row < countRow; row++)
                {
                    var name= worksheet.Cells[row,1].Value;
                    if (!string.IsNullOrEmpty(name))
                    {
                        double width = worksheet.Cells[row, 2].Value;
                        double height= worksheet.Cells[row, 3].Value;
                        BeamProperty beamPro= new BeamProperty(name, width, height);
                        properties.Add(beamPro);
                    }
                    else
                    {
                        break;
                    }
                }
                workbook.Close();
                appExecel.Quit();
                

            }




                return Result.Succeeded;
        }
    }
}
