using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace DVLD.Core
{
    public class clsImageHelper
    {

        private static readonly string FolderPath =
            Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName,"Images");

        public static string SaveImage(Image image, string nationalNo)
        {
            if (image == null)
                return string.Empty;


            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            string fileName = GenerateFileName(nationalNo);

            string fullPath = Path.Combine(FolderPath, fileName);

            try
            {

                image.Save(fullPath, ImageFormat.Jpeg);

                return fullPath;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string GenerateGUID()
        {
            return Guid.NewGuid().ToString();
        }

        private static string GenerateFileName(string nationalNo)
        {
            return $"{nationalNo}-{GenerateGUID()}.jpg";
        }

        public static bool DeleteImage(string imagePath)
        {
            try
            {
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

    }
}
