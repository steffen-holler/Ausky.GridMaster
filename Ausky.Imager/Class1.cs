using System;

namespace Ausky.Imager
{
    public class Grid
    {
        public double ImageWidth { get; set; } = 0;
        public double ImageHeight { get; set; }
        public double GridCountWidth { get; set; }
        public double GridCountHeight { get; set; }
        public double GridWidth
        { get { return ImageWidth / GridCountWidth; } }
        public double GridHeight 
        { get; private set; }
        
        
    }
}
