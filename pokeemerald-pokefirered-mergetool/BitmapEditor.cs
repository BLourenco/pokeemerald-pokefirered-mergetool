/// https://stackoverflow.com/questions/51071944/how-can-i-work-with-1-bit-and-4-bit-images

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool
{
    static class BitmapEditor
    {
        public static Bitmap SliceTiles(Bitmap main, out Bitmap slice)
        {
            int stride;
            byte[] mainBytesFull = GetImageData(main, out stride);
            byte[] mainBytesCropped = new byte[16384]; // Max bytes for 128x256px image with 4bpp
            byte[] slicedBytes = new byte[4096]; // Max bytes for 128x64px image with 4bpp

            // Copy cropped bytes
            for (int i = 0; i < 16384; i++)
            {
                mainBytesCropped[i] = mainBytesFull[i];
            }

            // Copy cropped bytes
            for (int i = 16384; i < mainBytesFull.Length; i++)
            {
                slicedBytes[i - 16384] = mainBytesFull[i];
            }

            Bitmap croppedMainTiles = BuildImage(mainBytesCropped, 128, 256, stride, PixelFormat.Format4bppIndexed);
            slice = BuildImage(slicedBytes, 128, 64, stride, PixelFormat.Format4bppIndexed);
            return croppedMainTiles;
        }

        public static Bitmap StitchTiles(Bitmap top, Bitmap bottom)
        {
            int strideTop;
            int strideBottom;
            byte[] topBytes = GetImageData(top, out strideTop);
            byte[] bottomBytes = GetImageData(bottom, out strideBottom);
            byte[] stitchedBytes = new byte[topBytes.Length + bottomBytes.Length];

            for (int i = 0; i < topBytes.Length; i++)
            {
                stitchedBytes[i] = topBytes[i];
            }

            for (int i = 0; i < bottomBytes.Length; i++)
            {
                stitchedBytes[i + topBytes.Length] = bottomBytes[i];
            }

            return BuildImage(stitchedBytes, 128, top.Height + bottom.Height, strideTop, PixelFormat.Format4bppIndexed);
        }

        /// <summary>
        /// Gets the raw bytes from an image.
        /// </summary>
        /// <param name="sourceImage">The image to get the bytes from.</param>
        /// <param name="stride">Stride of the retrieved image data.</param>
        /// <returns>The raw bytes of the image.</returns>
        private static byte[] GetImageData(Bitmap sourceImage, out int stride)
        {
            if (sourceImage == null)
                throw new ArgumentNullException("sourceImage", "Source image is null!");
            BitmapData sourceData = sourceImage.LockBits(new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), ImageLockMode.ReadOnly, sourceImage.PixelFormat);
            stride = sourceData.Stride;
            byte[] data = new byte[stride * sourceImage.Height];
            Marshal.Copy(sourceData.Scan0, data, 0, data.Length);
            sourceImage.UnlockBits(sourceData);
            return data;
        }

        /// <summary>
        /// Creates a bitmap based on data, width, height, stride and pixel format.
        /// </summary>
        /// <param name="sourceData">Byte array of raw source data</param>
        /// <param name="width">Width of the image</param>
        /// <param name="height">Height of the image</param>
        /// <param name="stride">Scanline length inside the data. If this is negative, the image is built from the bottom up (BMP format).</param>
        /// <param name="pixelFormat">Pixel format</param>
        /// <returns>The new image</returns>
        private static Bitmap BuildImage(byte[] sourceData, int width, int height, int stride, PixelFormat pixelFormat)
        {
            Bitmap newImage = new Bitmap(width, height, pixelFormat);
            BitmapData targetData = newImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, newImage.PixelFormat);
            int newDataWidth = ((Image.GetPixelFormatSize(pixelFormat) * width) + 7) / 8;
            // Compensate for possible negative stride on BMP format.
            bool isFlipped = targetData.Stride < 0;
            int targetStride = Math.Abs(targetData.Stride);
            long scan0 = targetData.Scan0.ToInt64();
            for (int y = 0; y < height; y++)
                Marshal.Copy(sourceData, y * stride, new IntPtr(scan0 + y * targetStride), newDataWidth);
            newImage.UnlockBits(targetData);
            // Fix negative stride on BMP format.
            if (isFlipped)
                newImage.RotateFlip(RotateFlipType.Rotate180FlipX);
            return newImage;
        }
    }
}
