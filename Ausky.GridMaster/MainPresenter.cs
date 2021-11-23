using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ausky.GridMaster
{
    internal class MainPresenter : Notify 
    {
        private string _imagePath = "";
        public string ImagePath
        {
            get { return _imagePath; }
            set 
            { 
                _imagePath = value; 
                OnPropertyChanged();
                LoadImage(value);
            }
        }

        private double _rotation = 0.0;
        public double Rotation
        {
            get { return _rotation; }  
            set { _rotation = value; OnPropertyChanged(); }
        }

        private int _columns = 3;
        public int Columns
        {
            get { return _columns; }   
            set 
            { 
                _columns = value; 
                OnPropertyChanged();
                UpdateGrid();
            }
        }

        private int _rows = 3;
        public int Rows
        {
            get { return _rows; }   
            set 
            {
                _rows = value; 
                OnPropertyChanged();
                UpdateGrid();
            }
        }

        public int Width
        {
            get 
            {
                if (_image == null) return 0;
                return (int)Math.Round(_image.Width, 0); 
            }
        }

        public int Height
        {
            get 
            {
                if (_image == null) return 0;
                return (int)Math.Round(_image.Height, 0); 
            }
        }

        private ImageSource _image = null;
        public ImageSource Image
        {
            get { return _image; }
            private set { _image = value; OnPropertyChanged();}
        }

        private ImageSource _gridLayer = null;
        public ImageSource GridLayer
        {
            get { return _gridLayer; }
            private set { _gridLayer = value; OnPropertyChanged();}
        }

        private void LoadImage(string source)
        {
            var uri = new Uri(source, UriKind.RelativeOrAbsolute);
            var bmp = new BitmapImage(uri);
            Image = bmp as ImageSource;
            OnPropertyChanged("Width");
            OnPropertyChanged("Height");

            UpdateGrid();
        }

        private void UpdateGrid()
        {
            if (Width <= 0 || Height <= 0)
                return;

            int bitDepth = 4;
            int gridWidth = (int)Math.Round(Image.Width / Columns, 0);
            int gridHeight = (int)Math.Round(Image.Height / Rows, 0);
            var dpi = 96.0;
            var stride = Width * bitDepth;
            byte[] data = new byte[bitDepth* Width * Height];

            for (int i = 0; i < data.Length; i += bitDepth)
            {
                int x = (i/bitDepth) % Width;
                int y = (i/bitDepth) / Width;

                if (y % gridHeight == 0)
                {
                    data[i] = 255;   // blue
                    data[i + 1] = 0; // green 
                    data[i + 2] = 255; // red
                    data[i + 3] = 255; // alpha
                }
                else if (x % gridWidth == 0)
                {
                    data[i] = 255;   // blue
                    data[i + 1] = 0; // green 
                    data[i + 2] = 0; // red
                    data[i + 3] = 255; // alpha
                }                
                else
                {
                    data[i] = 0;
                    data[i + 1] = 0;
                    data[i + 2] = 0;
                    data[i + 3] = 0;
                }
            }

            GridLayer = BitmapSource.Create(Width, Height, dpi, dpi, PixelFormats.Bgra32, null, data, stride);
        }
    }
}
