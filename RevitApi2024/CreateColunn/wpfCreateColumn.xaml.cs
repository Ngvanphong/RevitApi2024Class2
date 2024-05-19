using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RevitApi2024.CreateColunn
{
    /// <summary>
    /// Interaction logic for wpfCreateColumn.xaml
    /// </summary>
    public partial class wpfCreateColumn : Window
    {
        private readonly ExternalEvent _createColumnEvent;
        
        public wpfCreateColumn(ExternalEvent createColumnEvent)
        {
            InitializeComponent();
            _createColumnEvent = createColumnEvent;
        }

        private void btnPickPoint(object sender, RoutedEventArgs e)
        {
            _createColumnEvent.Raise();
        }
    }
}
