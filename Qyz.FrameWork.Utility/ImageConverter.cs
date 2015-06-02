using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Qyz.FrameWork.Utility
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string imagePath = string.Empty;
            if (value == null || value.ToString() == "")
            {
                imagePath = "config.png";
            }
            else
            {
                imagePath = value.ToString();
            }
            string path = Environment.CurrentDirectory + "/Image/" + imagePath;
            if (File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                return BitmapFrame.Create(fs);
            }
            return null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
