///////////////////////////////////////////////////////////////////////////////////////////////
//
//    This File is Part of the CallButler Open Source PBX (http://www.codeplex.com/callbutler
//
//    Copyright (c) 2005-2008, Jim Heising
//    All rights reserved.
//
//    Redistribution and use in source and binary forms, with or without modification,
//    are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice,
//      this list of conditions and the following disclaimer.
//
//    * Redistributions in binary form must reproduce the above copyright notice,
//      this list of conditions and the following disclaimer in the documentation and/or
//      other materials provided with the distribution.
//
//    * Neither the name of Jim Heising nor the names of its contributors may be
//      used to endorse or promote products derived from this software without specific prior
//      written permission.
//
//    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//    ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
//    IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
//    INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
//    NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//    PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
//    WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
//    ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
//    POSSIBILITY OF SUCH DAMAGE.
//
///////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace WOSI.Utilities
{
    public class ImageUtils
    {
        public static Image CreateThumbnailImage(int width, int height, Image image, bool center)
        {
            // Create our new image
            Bitmap newImage = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                if (center && image.Width != image.Height)
                {
                    Rectangle srcRect = new Rectangle();

                    if (image.Width > image.Height)
                    {
                        srcRect.Width = image.Height;
                        srcRect.Height = image.Height;
                        srcRect.X = (image.Width - image.Height) / 2;
                        srcRect.Y = 0;
                    }
                    else
                    {
                        srcRect.Width = image.Width;
                        srcRect.Height = image.Width;
                        srcRect.Y = (image.Height - image.Width) / 2;
                        srcRect.X = 0;
                    }

                    g.DrawImage(image, new Rectangle(0, 0, newImage.Width, newImage.Height), srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel);
                }
                else
                {
                    g.DrawImage(image, 0, 0, width, height);
                }
            }

            return newImage;
        }

        public static Image CreateThumbnailImage(int size, Image image, bool center)
        {
            return CreateThumbnailImage(size, size, image, center);
        }

        public static Image GetImageFromBytes(byte[] imageBytes)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image image = Image.FromStream(ms);

                    return image;
                }
            }
            catch
            {
                return null;
            }
        }

        public static byte[] GetImageBytes(Image image, ImageFormat format)
        {
            byte[] imageBytes;

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, format);
                    imageBytes = ms.ToArray();

                    return imageBytes;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
