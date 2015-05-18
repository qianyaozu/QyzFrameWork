using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace Qyz.FrameWork.Utility.ImageOperate
{
    /// <summary>
    /// 图片压缩处理
    /// </summary>
    public class ImageCompress : IDisposable
    {
        #region Fields
        MemoryStream Memory;

        #endregion

        public ImageCompress()
        {
        }

        public ImageCompress(Image imageSource)
        {
            this.ImageSource = imageSource;
        }

        #region Properties

        /// <summary>
        /// 图片数据源
        /// </summary>
        public Image ImageSource { get; set; }

        /// <summary>
        /// 图片文件类型
        /// </summary>
        public ImageType FileType
        {
            get
            {
                return this._FileType;
            }
            set
            {
                this._FileType = value;
            }
        }
        ImageType _FileType = ImageType.Jpg;

        public Size FixedSize { get; set; }

        public bool IsNeedZoom { get; private set; }

        #endregion


        #region Methods

        #region Public
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="qualityLevel">0 - 100</param>
        /// <returns></returns>
        public Image CompressedToImage(Size size, long qualityLevel)
        {
            //压缩图片到内存
            this.Compressed(size, qualityLevel);

            return Image.FromStream(this.Memory);
        }

        public byte[] CompressedToBytes(Size size, long qualityLevel)
        {
            //压缩图片到内存
            this.Compressed(size, qualityLevel);

            return this.Memory.ToArray();
        }

        public Image CompressedToImage(long qualityLevel)
        {
            //压缩图片到内存
            this.Compressed(qualityLevel);

            return Image.FromStream(this.Memory);
        }

        public byte[] CompressedToBytes(long qualityLevel)
        {
            //压缩图片到内存
            this.Compressed(qualityLevel);

            return this.Memory.ToArray();
        }

        public Size GetZoomSize(int width,int height)
        {
            Size newSize=new Size();
            Size fixedSize=this.FixedSize;
            if (fixedSize == null) throw new Exception("Fixedsize property to a null value");


            if(width<=fixedSize.Width && height<=fixedSize.Height)
            {
                newSize.Width = width;
                newSize.Height = height; 
            }
            else
            {
                // 取得比例系数
                float w = fixedSize.Width / (float)width;
                float h = fixedSize.Height / (float)height;
                // 宽度比大于高度比
                if (w > h)
                {
                    newSize.Width = w >= 1 ? width : fixedSize.Width;
                    newSize.Height = (int)(w >= 1 ? Math.Round(height / w) : Math.Round(height * w));
                }
                // 宽度比小于高度比
                else if (w < h)
                {
                    newSize.Height = h >= 1 ? height : fixedSize.Height;
                    newSize.Width = (int)(h >= 1 ? Math.Round(width / h) : Math.Round(width * h));
                }
                // 宽度比等于高度比
                else
                {
                    newSize.Width = fixedSize.Width;
                    newSize.Height = fixedSize.Height;
                }
            }

            return newSize;
        }

        

        public void Dispose()
        {
            if (this.Memory != null) this.Memory.Dispose();
            if (this.ImageSource != null) this.ImageSource.Dispose();
        }

        #endregion

        #region Private
        private void Compressed(long qualityLevel)
        {
            //验证参数
            this.ValidParms(this.ImageSource, qualityLevel);

            using (Bitmap bitmap = new Bitmap(this.ImageSource))
            {
                //设置压缩质量
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityLevel);
                ImageCodecInfo imgCodeInfo = this.GetEncoderInfo(this.ToMimeType());

                //释放已存的内存资源
                if (this.Memory != null)
                    this.Memory.Dispose();

                //开辟新的内存
                this.Memory = new MemoryStream();

                //压缩图片至内存
                bitmap.Save(this.Memory, imgCodeInfo, encoderParams);
            }
        }

        private void Compressed(Size size, long qualityLevel)
        {
            //验证参数
            this.ValidParms(this.ImageSource, qualityLevel);

            using (Bitmap bitMap = new Bitmap(size.Width, size.Height))
            {
                using (Graphics g = Graphics.FromImage(bitMap))
                {
                    //设置画布的描绘质量
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    //绘制图像到新的画布
                    g.DrawImage(this.ImageSource, new Rectangle(0, 0, size.Width, size.Height), 0, 0, this.ImageSource.Width, this.ImageSource.Height, GraphicsUnit.Pixel);
                }

                //设置压缩质量
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityLevel);

                //释放已存的内存资源
                if (this.Memory != null)
                    this.Memory.Dispose();

                //开辟新的内存
                this.Memory = new MemoryStream();

                //压缩图片至内存
                bitMap.Save(this.Memory, this.GetEncoderInfo(this.ToMimeType()), encoderParams);
            }
        }

        private ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            for (int j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private string ToMimeType()
        {
            switch (this.FileType)
            {
                case ImageType.Bmp:
                    return "image/bmp";
                case ImageType.Gif:
                    return "image/gif";
                case ImageType.Png:
                    return "image/png";
                default:
                    return "image/jpeg";
            }
        }

        void ValidParms(Image image, double qualityLevel)
        {
            if (image == null) throw new ArgumentNullException("ImageSource 属性未赋值，不能对图像进行处理。");
            if (qualityLevel > 100 || qualityLevel < 0)
                throw new Exception("图片处理质量值只能在 0 - 100 范围内。");
        }
        #endregion

        #endregion
    }
}
