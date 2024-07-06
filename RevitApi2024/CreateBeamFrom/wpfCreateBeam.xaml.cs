using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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
using Autodesk.Revit.UI;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace RevitApi2024.CreateBeamFrom
{
    /// <summary>
    /// Interaction logic for wpfCreateBeam.xaml
    /// </summary>
    public partial class wpfCreateBeam : FluentWindow
    {
        private readonly ExternalEvent _pickPointEvent;
        private readonly ExternalEvent _createBeamEvent;

        public event PropertyChangedEventHandler PropertyChanged;

        private bool isEnableOkButton;
        public bool IsEnableOkButton
        {
            get { return isEnableOkButton; }
            set
            {
                isEnableOkButton = value;
                OnPropertyChanged("IsEnableOkButton");
            }
        }

        public wpfCreateBeam(ExternalEvent pickPointEvent, ExternalEvent createBeamEvent)
        {
            InitializeComponent();
            //ApplicationThemeManager.Apply(this);
            
        }

        private void btnPickPoint(object sender, RoutedEventArgs e)
        {
            _pickPointEvent.Raise();

        }

        private void btnOk(object sender, RoutedEventArgs e)
        {
            //DialogResult = true;
            //this.Close();
            _createBeamEvent.Raise();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
    }
}
