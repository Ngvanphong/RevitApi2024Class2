using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitApi2024
{
    public class Data
    {
        public Data(long id, string name,Information information)
        {
            (Id,Name,Information)=(id,name,information);
        }


        public long Id { get; set; }    
        public string Name { get; set; }

        public Information Information { get; set; }


    }
}
