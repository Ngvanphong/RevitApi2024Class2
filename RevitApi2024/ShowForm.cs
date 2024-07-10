using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitApi2024
{
    public static class ShowForm
    {
        public static ProgressBarWpf ProgressBarWpf { get; set; }
        public static void Show()
        {
            ProgressBarWpf = new ProgressBarWpf();
            ProgressBarWpf.Show();
        }
    }
}
