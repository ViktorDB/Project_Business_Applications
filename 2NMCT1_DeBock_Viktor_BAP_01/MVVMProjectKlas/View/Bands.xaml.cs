using Microsoft.Win32;
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

namespace MVVMProjectKlas.View
{
    /// <summary>
    /// Interaction logic for Bands.xaml
    /// </summary>
    public partial class Bands : UserControl
    {
        public Bands()
        {
            InitializeComponent();
        }

        private void insertImage_Drop(object sender, DragEventArgs e)
        {
            string[] fileSource = (string[])e.Data.GetData(DataFormats.FileDrop);
            insertImage.Source = new BitmapImage(new Uri(fileSource[0], UriKind.RelativeOrAbsolute));           
            //url = fileSource[0];
        }
        


    }
}
