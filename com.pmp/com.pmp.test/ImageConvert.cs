using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.test
{
    class ImageConvert
    {
        //图片宽度，单位 px
        private readonly int imageMaxWidth = 600;
        //图片大小阈值 单位 k
        private readonly int imageMaxSize = 100;
        //图片压缩比例1-100
        private readonly int imageQuality = 100;

        public void CompressImage(string fileRoot)
        {
            if (!Directory.Exists(fileRoot))
                return;
            var files = Directory.GetFiles(fileRoot);
            if (files == null || files.Length < 1) return;
            foreach (var file in files)
            {
                var newFile = RenameFile(file);
                var flag = CompressImageWidth(newFile, file, imageMaxWidth);
                if (!flag) RenameFile(newFile, true);

                CompressImageQuality(file, imageQuality - 10);

            }

        }


        private string RenameFile(string filePath, bool roolback = false)
        {
            string fileDirectory = Path.GetDirectoryName(filePath);
            var suffix = Path.GetExtension(filePath);
            var fileName = Path.GetFileName(filePath).Replace(suffix, "");
            var newFilePath = $"{fileDirectory}\\{fileName}_temp{suffix}";
            if (roolback)
                newFilePath = filePath.Replace("_temp", "");
            if (File.Exists(filePath))
            {
                File.Move(filePath, newFilePath);
                return newFilePath;
            }
            return null;
        }




        private bool CompressImageWidth(string sourceFilePath, string newFilePath, int maxWidth)
        {
            Image image = Image.FromFile(sourceFilePath);
            ImageFormat format = image.RawFormat;
            var width = image.Width;
            var height = image.Height;

            int maxHeight = height;
            if (width <= maxWidth)
            {
                image.Dispose();
                return false;
            }
            else
                maxHeight = maxWidth / width * height;

            Bitmap bitmap = new Bitmap(maxWidth, maxHeight);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(image, new Rectangle(0, 0, maxWidth, maxHeight), 0, 0, width, height, GraphicsUnit.Pixel);
            g.Dispose();

            //以下代码为保存图片时，设置压缩质量
            var quality = 100;//设置压缩的比例1-100
            var qualityParam = new long[] { quality };
            EncoderParameters encoder = new EncoderParameters()
            {
                Param = new EncoderParameter[]
                {
                    new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam),
                }
            };
            try
            {
                bitmap.Save(newFilePath, format);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            finally
            {
                image.Dispose();
                bitmap.Dispose();
            }
        }

        private bool CompressImageQuality(string sourceFilePath, int quality)
        {
            FileInfo fileInfo = new FileInfo(sourceFilePath);
            if (fileInfo.Length < imageMaxSize * 1024)
                return true;
            var newFilePath = sourceFilePath;
            sourceFilePath = RenameFile(sourceFilePath);
            if (sourceFilePath == null) return false;

            Image image = Image.FromFile(sourceFilePath);
            ImageFormat format = image.RawFormat;
            Bitmap bitmap = new Bitmap(image);

            //以下代码为保存图片时，设置压缩质量
            var qualityParam = new long[] { quality };
            EncoderParameters encoder = new EncoderParameters()
            {
                Param = new EncoderParameter[]
                {
                    new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityParam),
                }
            };
            try
            {
                bitmap.Save(newFilePath, format);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            finally
            {
                image.Dispose();
                bitmap.Dispose();
            }

        }

    }
}
