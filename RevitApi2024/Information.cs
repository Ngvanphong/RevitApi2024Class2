using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitApi2024
{
    public class Information
    {
        public Information(Point location,string material)
        {
            (Location,Material)=(location, material);
        }
        public Point Location { get; set; }
        public string Material {  get; set; }
    }
    public class  Point
    {
        public Point(double x, double y , double z)
        {
            (X,Y,Z)=(x,y,z);
        }
        public double X { set; get; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}
