using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ausky.GridMaster
{
    internal class MainPresenter : ObservableObject 
    {
        #region member variables
        private string _imagePath = "";
        private double _rotation = 0.0;
        private int _majorColumns = 3;
        private int _majorRows = 3;
        private int _minorColumns = 8;
        private int _minorRows = 8;
        private ImageSource _image = null;
        private ImageSource _gridLayer = null;
        #endregion

        #region properties
        public string ImagePath
        {
            get { return _imagePath; }
            set 
            { 
                if (SetProperty(ref _imagePath, value))
                    LoadImage(value);   
            }
        }

        public double Rotation
        {
            get { return _rotation; }  
            set { SetProperty(ref _rotation, value); }
        }

        public int MajorColumns
        {
            get { return _majorColumns; }   
            set 
            { 
                if (SetProperty(ref _majorColumns, value))
                    UpdateGrid();
            }
        }

        public int MajorRows
        {
            get { return _majorRows; }   
            set 
            {
                _majorRows = value; 
                OnPropertyChanged();
                UpdateGrid();
            }
        }

        public int MinorColumns
        {
            get { return _minorColumns; }
            set
            {
                if (SetProperty(ref _minorColumns, value))
                    UpdateGrid();
            }
        }

        public int MinorRows
        {
            get { return _minorRows; }
            set
            {
                if (SetProperty(ref _minorRows, value))
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

        public ImageSource Image
        {
            get { return _image; }
            private set { SetProperty(ref _image, value); }
        }

        public ImageSource GridLayer
        {
            get { return _gridLayer; }
            private set { SetProperty(ref _gridLayer, value); }
        }
        #endregion

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
            int gridWidth = (int)Math.Round(Image.Width / MajorColumns, 0);
            int smallGridWidth = (int)Math.Round((double)gridWidth / MinorColumns, 0);
            int gridHeight = (int)Math.Round(Image.Height / MajorRows, 0);
            int smallGridHeight = (int)Math.Round((double)gridHeight / MinorRows, 0);
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
                else if (y % smallGridHeight == 0)
                {
                    data[i] = 255;     // blue
                    data[i + 1] = 255; // green 
                    data[i + 2] = 0; // read
                    data[i + 3] = 255; // alpha
                }
                else if (x % gridWidth == 0)
                {
                    data[i] = 255;   // blue
                    data[i + 1] = 0; // green 
                    data[i + 2] = 255; // red
                    data[i + 3] = 255; // alpha
                }
                else if (x % smallGridWidth == 0)
                {
                    data[i] = 255;
                    data[i + 1] = 255;
                    data[i + 2] = 0;
                    data[i + 3] = 255;
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
