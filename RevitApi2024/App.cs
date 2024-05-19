using Autodesk.Revit.UI;
using RevitApi2024.Button;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitApi2024
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            CreateBeamButton createBeamButton = new CreateBeamButton();
            createBeamButton.CreateBeam(a);

            CreateBeam2Button createBeam2Button= new CreateBeam2Button();
            createBeam2Button.CreateBeam2(a);

            CreateColumnButton createColumnButton = new CreateColumnButton();
            createColumnButton.CreateColumn(a);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {

            return Result.Succeeded;
        }
    }
}
