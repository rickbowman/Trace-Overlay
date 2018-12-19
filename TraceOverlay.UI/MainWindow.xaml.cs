using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TraceOverlay.UI
{
    public partial class MainWindow : Window
    {
        public BitmapImage imagePreview { get; set; }
        public Bitmap image { get; set; }
        public TraceWindow tw { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ToSecondaryScreen();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            if (tw != null)
            {
                image = null;
                chkDisplayOverlay.IsChecked = false;
            }

            Microsoft.Win32.OpenFileDialog op = new Microsoft.Win32.OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imagePreview = new BitmapImage(new Uri(op.FileName));
                image = new Bitmap(op.FileName);
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = imagePreview;
                splPreview.Background = ib;
            }
        }

        private void ToSecondaryScreen()
        {
            var secondaryScreen = Screen.AllScreens.FirstOrDefault(s => !s.Primary);
            if (secondaryScreen != null)
            {
                Left = 1912;
                Top = secondaryScreen.Bounds.Top;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tw != null)
                tw.Close();
        }

        private void chkDisplayOverlay_Checked(object sender, RoutedEventArgs e)
        {
            if (image != null)
            {
                tw = new TraceWindow(image);
                tw.Show(); 
            }
        }

        private void chkDisplayOverlay_Unchecked(object sender, RoutedEventArgs e)
        {
            if (tw != null)
            {
                tw.Close();
                tw = null; 
            }
        }
    }
}
