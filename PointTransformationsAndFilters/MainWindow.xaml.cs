using Microsoft.Win32;
using PointTransformationsAndFilters.Enums;
using PointTransformationsAndFilters.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

namespace PointTransformationsAndFilters
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                Orginal.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                AfterFiltering.Source = Orginal.Source;
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            byte value;
            if (!byte.TryParse(Adding.Text, out value))
            {
                MessageBox.Show("Niepoprawna wartość. Podaj wartość od 0 do 255");
                Adding.Text = "0";
                return;
            }
            BitmapImage image = Orginal.Source as BitmapImage;
            Bitmap bitmap = BitmapConverter.GetBitmapFromBitmapImage(image);
            Scale2.Value = 1;
            AfterFiltering.Source = ConvertBitmapToBitmapImage(BitmapConverter.Add(bitmap, value));
        }

        private void Subtract(object sender, RoutedEventArgs e)
        {
            byte value;
            if (!byte.TryParse(Subtracting.Text, out value))
            {
                MessageBox.Show("Niepoprawna wartość. Podaj wartość od 0 do 255");
                Subtracting.Text = "0";
                return;
            }
            BitmapImage image = Orginal.Source as BitmapImage;
            Bitmap bitmap = BitmapConverter.GetBitmapFromBitmapImage(image);
            Scale2.Value = 1;
            AfterFiltering.Source = ConvertBitmapToBitmapImage(BitmapConverter.Subtract(bitmap, value));
        }

        private void Multiply(object sender, RoutedEventArgs e)
        {
            double value;
            if (!double.TryParse(Multipling.Text, out value) || value > 255.0)
            {
                MessageBox.Show("Niepoprawna wartość. Podaj wartość od 0 do 255");
                Multipling.Text = "0";
                return;
            }
            BitmapImage image = Orginal.Source as BitmapImage;
            Bitmap bitmap = BitmapConverter.GetBitmapFromBitmapImage(image);
            Scale2.Value = 1;
            AfterFiltering.Source = ConvertBitmapToBitmapImage(BitmapConverter.Multiply(bitmap, value));
        }

        private void Divide(object sender, RoutedEventArgs e)
        {
            double value;
            if (!double.TryParse(Dividing.Text, out value) || value > 255.0 || value == 0.0)
            {
                MessageBox.Show("Niepoprawna wartość");
                Dividing.Text = "0";
                return;
            }
            BitmapImage image = Orginal.Source as BitmapImage;
            Bitmap bitmap = BitmapConverter.GetBitmapFromBitmapImage(image);
            Scale2.Value = 1;
            AfterFiltering.Source = ConvertBitmapToBitmapImage(BitmapConverter.Divide(bitmap, value));
        }

        private void Brightness(object sender, RoutedEventArgs e)
        {
            double value;
            if (!double.TryParse(BrightnessChanging.Text, out value))
            {
                MessageBox.Show("Niepoprawna wartość");
                BrightnessChanging.Text = "100";
                return;
            }
            BitmapImage image = Orginal.Source as BitmapImage;
            Bitmap bitmap = BitmapConverter.GetBitmapFromBitmapImage(image);
            Scale2.Value = 1;
            AfterFiltering.Source = ConvertBitmapToBitmapImage(BitmapConverter.ChangeBrightness(bitmap, value/100));
        }

        private void ConvertToGrayScaleAverage(object sender, RoutedEventArgs e)
        {
            BitmapImage image = Orginal.Source as BitmapImage;
            Bitmap bitmap = BitmapConverter.GetBitmapFromBitmapImage(image);
            Scale2.Value = 1;
            AfterFiltering.Source = ConvertBitmapToBitmapImage(BitmapConverter.ConvertToGrayScaleAverage(bitmap));
        }

        private void ConvertToGrayScaleYUV(object sender, RoutedEventArgs e)
        {
            BitmapImage image = Orginal.Source as BitmapImage;
            Bitmap bitmap = BitmapConverter.GetBitmapFromBitmapImage(image);
            Scale2.Value = 1;
            AfterFiltering.Source = ConvertBitmapToBitmapImage(BitmapConverter.ConvertToGrayScaleYUV(bitmap));
        }

        private void AverageFilter(object sender, RoutedEventArgs e)
        {
            BitmapImage image = Orginal.Source as BitmapImage;
            Bitmap bitmap = BitmapConverter.GetBitmapFromBitmapImage(image);
            Scale2.Value = 1;
            AfterFiltering.Source = ConvertBitmapToBitmapImage(BitmapConverter.AverageFilter(bitmap));
        }
        private BitmapImage ConvertBitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();

                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        private void Slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Orginal.LayoutTransform = new ScaleTransform(e.NewValue, e.NewValue);
        }

        private void Slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            AfterFiltering.LayoutTransform = new ScaleTransform(e.NewValue, e.NewValue);
        }

        private void MedianFilter(object sender, RoutedEventArgs e)
        {
            BitmapImage image = Orginal.Source as BitmapImage;
            Bitmap bitmap = BitmapConverter.GetBitmapFromBitmapImage(image);
            Scale2.Value = 1;
            AfterFiltering.Source = ConvertBitmapToBitmapImage(BitmapConverter.MedianFilter(bitmap));
        }

        private void SobelX(object sender, RoutedEventArgs e)
        {
            BitmapImage image = Orginal.Source as BitmapImage;
            Bitmap bitmap = BitmapConverter.GetBitmapFromBitmapImage(image);
            Scale2.Value = 1;
            AfterFiltering.Source = ConvertBitmapToBitmapImage(BitmapConverter.SobelFilter(bitmap,SobelFilterVariantEnum.X));
        }

        private void SobelY(object sender, RoutedEventArgs e)
        {
            BitmapImage image = Orginal.Source as BitmapImage;
            Bitmap bitmap = BitmapConverter.GetBitmapFromBitmapImage(image);
            Scale2.Value = 1;
            AfterFiltering.Source = ConvertBitmapToBitmapImage(BitmapConverter.SobelFilter(bitmap, SobelFilterVariantEnum.Y));
        }

        private void SobelXY(object sender, RoutedEventArgs e)
        {
            BitmapImage image = Orginal.Source as BitmapImage;
            Bitmap bitmap = BitmapConverter.GetBitmapFromBitmapImage(image);
            Scale2.Value = 1;
            AfterFiltering.Source = ConvertBitmapToBitmapImage(BitmapConverter.SobelFilter(bitmap, SobelFilterVariantEnum.XY));
        }

        private void HighPassFilter(object sender, RoutedEventArgs e)
        {
            BitmapImage image = Orginal.Source as BitmapImage;
            Bitmap bitmap = BitmapConverter.GetBitmapFromBitmapImage(image);
            Scale2.Value = 1;
            AfterFiltering.Source = ConvertBitmapToBitmapImage(BitmapConverter.HighPassFilter(bitmap));
        }

        private void GausianFilter(object sender, RoutedEventArgs e)
        {
            BitmapImage image = Orginal.Source as BitmapImage;
            Bitmap bitmap = BitmapConverter.GetBitmapFromBitmapImage(image);
            Scale2.Value = 1;
            AfterFiltering.Source = ConvertBitmapToBitmapImage(BitmapConverter.GausianFilter(bitmap));
        }
    }
}
