using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ausky.Imager
{
    /// <summary>
    /// This class contains methods for mutating and changing an image, and is used
    /// primarily for encapsulating Image manipulation that is found in the System.
    /// Windows.Media and System.Windows.Media.Imaging libraries.
    /// </summary>
    public static class ImageManipulation
    {
        /// <summary>
        /// Preforms an image rotation between -360 to 360 degrees on the source image
        /// and will return a new image with the rotation applied. 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static BitmapImage RotateImage(BitmapImage source, double degrees)
        {
            // Verify that our angle provided is within our bounds.
            if (degrees > 360 || degrees < -360)
                throw new ArgumentOutOfRangeException(nameof(degrees), 
                    "Angle must be between -360° and +360°. supplied angle is was : " 
                    + degrees + "°.");
            // verify that our source is not null. 
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            
            TransformedBitmap tfedBmp = new TransformedBitmap();


            throw new NotImplementedException();
        }
    }
}
