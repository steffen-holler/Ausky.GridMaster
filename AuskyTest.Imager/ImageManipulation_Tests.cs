using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Ausky.Imager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuskyTest.Imager
{
    [TestClass]
    public class ImageManipulation_Tests
    {
        #region Rotate(BitmapImage image, double degrees)
        /// <summary>
        /// Tests that the image rotation will only accept images that are between -360 to +360 
        /// degrees. 
        /// </summary>
        [TestMethod]
        public void Rotate_RotationBounds()
        {
            double epsilonOutNegative = -360.000000000000056843418860808;
            double epsilonOutPositive = 360.000000000000056843418860808;

            // check the smallest increment out of bounds
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            ImageManipulation.RotateImage(null, epsilonOutNegative));
            
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            ImageManipulation.RotateImage(null, epsilonOutPositive));
            
            try { Rotate_CheckInboundsThrowsNoOutOfBoundsException(null, -360); }
            catch (Exception ex) { Debug.WriteLine("Warning: " + ex.Message); }

            try { Rotate_CheckInboundsThrowsNoOutOfBoundsException(null, -88); }
            catch (Exception ex) { Debug.WriteLine("Warning: " + ex.Message); }

            try { Rotate_CheckInboundsThrowsNoOutOfBoundsException(null, 0.0); }
            catch (Exception ex) { Debug.WriteLine("Warning: " + ex.Message); }

            try { Rotate_CheckInboundsThrowsNoOutOfBoundsException(null, 88); }
            catch (Exception ex) { Debug.WriteLine("Warning: " + ex.Message); }

            try { Rotate_CheckInboundsThrowsNoOutOfBoundsException(null, 360); }
            catch (Exception ex) { Debug.WriteLine("Warning: " + ex.Message); }
        }

        /// <summary>
        /// Tests that a null image will throw back an <see cref="ArgumentNullException"/>
        /// 
        /// </summary>
        [TestMethod]
        public void Rotate_CheckForNullImage()
        {
            var image = GenerateCheckerboardImage();
            Assert.ThrowsException<ArgumentNullException>(() =>
            ImageManipulation.RotateImage(null, 0.0));

            try { ImageManipulation.RotateImage(image, 0.0); }
            catch (ArgumentNullException) { Assert.Fail(); }
            catch (Exception ex) { Debug.WriteLine("Warning: " + ex.Message); }
        }

        /// <summary>
        /// Checks that degrees given does not throw a <see cref="ArgumentOutOfRangeException"/>
        /// but does not 
        /// </summary>
        /// <param name="image">Image to preform the rotation on</param>
        /// <param name="rotation">Rotation amount to be tested</param>
        private BitmapImage Rotate_CheckInboundsThrowsNoOutOfBoundsException(BitmapImage? image, double rotation)
        {
            try
            {
                return ImageManipulation.RotateImage(null, rotation);
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.Fail();
                return null;
            }
        }
        #endregion

        #region helper methods
        private BitmapImage GenerateCheckerboardImage()
        {
            byte[] data = new byte[75]
            //      0      ,       1      ,       2      ,       3      ,       4
            { 255, 255, 255, 000, 000, 000, 255, 255, 255, 000, 000, 000, 255, 255, 255,
              000, 000, 000, 255, 255, 255, 000, 000, 000, 255, 255, 255, 000, 000, 000,
              255, 255, 255, 000, 000, 000, 255, 255, 255, 000, 000, 000, 255, 255, 255,
              000, 000, 000, 255, 255, 255, 000, 000, 000, 255, 255, 255, 000, 000, 000,
              255, 255, 255, 000, 000, 000, 255, 255, 255, 000, 000, 000, 255, 255, 255 };

            return GenerateImage(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        /// <remarks>
        /// Pulled from the response from:
        /// https://stackoverflow.com/questions/14337071/convert-array-of-bytes-to-bitmapimage/14337202
        /// </remarks>
        private BitmapImage GenerateImage(byte[] array)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            using (MemoryStream ms = new MemoryStream(array))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                ms.Position = 0;
                image.CacheOption = BitmapCacheOption.Default;
                image.StreamSource = ms;
                image.EndInit();
                if (image.CanFreeze)
                    image.Freeze();
                return image;
            }
        }
        #endregion
    }
}
