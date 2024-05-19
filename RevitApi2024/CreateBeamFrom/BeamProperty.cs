using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RevitApi2024.CreateBeamFrom
{
    public class BeamProperty
    {
        public BeamProperty(string name, double width, double height)
        {
            (Name,Width,Height)=(name,width,height);
        }
        public string Name { get; set; }    
        public double Width {  get; set; }
        public double Height { get; set; }  
    }
}
