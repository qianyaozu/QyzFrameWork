using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Interop;


namespace Qyz.FrameWork.Utility.ImageOperate
{
    public class ImageUtil
    {
        /// <summary>
        /// 生成图片缩略图
        /// </summary>
        /// <param name="originalImage">图片源</param>
        /// <param name="width">生成的宽度</param>
        /// <param name="height">生成的高度</param>
        /// <returns>返回生成的缩略图</returns>
        public static Image MakeThumbnail(Image originalImage, int width, int height)
        {
            int towidth = 0;
            int toheight = 0;
            if (originalImage.Width > width && originalImage.Height < height)
            {
                towidth = width;
                toheight = originalImage.Height;
            }

            if (originalImage.Width < width && originalImage.Height > height)
            {
                towidth = originalImage.Width;
                toheight = height;
            }
            if (originalImage.Width > width && originalImage.Height > height)
            {
                towidth = width;
                toheight = height;
            }
            if (originalImage.Width < width && originalImage.Height < height)
            {
                towidth = originalImage.Width;
                toheight = originalImage.Height;
            }

            //新建一个bmp图片 
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板 
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //清空画布并以透明背景色填充 
            g.Clear(Color.Transparent);

            //System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, towidth, toheight);
            ////在指定位置并且按指定大小绘制原图片的指定部分 
            //g.DrawImage(originalImage, rectDestination, 0, 0, towidth, toheight, GraphicsUnit.Pixel);

            g.DrawImage(originalImage, 0, 0, towidth, toheight);

            try
            {
                return bitmap;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                //bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        /// 放大缩小图片
        /// </summary>
        /// <param name="bmp">源图片</param>
        /// <param name="newW">宽度</param>
        /// <param name="newH">高度</param>
        /// <returns></returns>
        public static Bitmap ResizeImage(Image bmp, int newW, int newH)
        {
            try
            {
                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                //设置高质量,低速度呈现平滑程度 
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //设置高质量插值法 
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                //清空画布并以透明背景色填充 
                g.Clear(Color.Transparent);
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 图片转字节流
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] Image2Byte(Image image)
        {
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                image.Save(stream, image.RawFormat);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// 字节流转图片
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static Image Byte2Image(Byte[] buffer)
        {
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                stream.Write(buffer, 0, buffer.Length);
                return Image.FromStream(stream);
            }
        }


        public static System.Drawing.Bitmap BytesToBitmap(byte[] Bytes)
        {
            System.IO.MemoryStream stream = null;
            try
            {
                stream = new System.IO.MemoryStream(Bytes);
                return new System.Drawing.Bitmap((System.Drawing.Image)new System.Drawing.Bitmap(stream));
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
        }


        /// <summary>
        /// 以逆时针为方向对图像进行旋转
        /// </summary>
        /// <param name="b">位图流</param>
        /// <param name="angle">旋转角度[0,360](前台给的)</param>
        /// <returns></returns>
        public static Bitmap Rotate(Bitmap b, int angle)
        {
            angle = angle % 360;            //弧度转换
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            //原图的宽和高
            int w = b.Width;
            int h = b.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));
            //目标位图
            Bitmap dsImage = new Bitmap(W, H);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //计算偏移量
            Point Offset = new Point((W - w) / 2, (H - h) / 2);
            //构造图像显示区域：让图像的中心与窗口的中心点一致
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(360 - angle);
            //恢复图像在水平和垂直方向的平移
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawImage(b, rect);
            //重至绘图的所有变换
            g.ResetTransform();
            g.Save();
            g.Dispose();
            //dsImage.Save("yuancd.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            return dsImage;
        }

        public static BitmapSource ChangeBitmapToBitmapSource(Bitmap bmp)
        {
            BitmapSource returnSource;
            try
            {
                returnSource = Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            catch
            {
                returnSource = null;
            }
            return returnSource;
        }

        /// <summary>
        ///  byte[]转换为bitmapimage
        /// </summary>
        /// <param name="bytearray"></param>
        /// <returns></returns>
        public static System.Windows.Media.Imaging.BitmapImage BytearrayToBitmapImage(byte[] bytearray)
        {
            System.Windows.Media.Imaging.BitmapImage bmp = null;
            try
            {
                bmp = new System.Windows.Media.Imaging.BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new System.IO.MemoryStream(bytearray);
                bmp.EndInit();
            }
            catch
            {
                bmp = null;
            }
            return bmp;
        }

        /// <summary>
        /// bitmapimage转换为byte[]
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static byte[] BitmapImageToBytearray(System.Windows.Media.Imaging.BitmapImage bmp)
        {
            byte[] bytearray = null;
            try
            {
                System.IO.Stream smarket = bmp.StreamSource;

                if (smarket != null && smarket.Length > 0)
                {
                    //很重要，因为position经常位于stream的末尾，导致下面读取到的长度为0。 
                    smarket.Position = 0;

                    using (System.IO.BinaryReader br = new System.IO.BinaryReader(smarket))
                    {
                        bytearray = br.ReadBytes((int)smarket.Length);
                    }
                }
            }
            catch
            {
                //other exception handling 
            }
            return bytearray;
        }

        public static System.Drawing.Bitmap WpfBitmapSourceToBitmap(BitmapSource s)
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(s.PixelWidth, s.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            System.Drawing.Imaging.BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            s.CopyPixels(System.Windows.Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
            bmp.UnlockBits(data);
            return bmp;
        }
    }
}
