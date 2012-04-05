using System;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Library
{
    public static class ImageUtil
    {
          public static void CheckImage(string fileName, int width, int height)
        {
            try
            {
                // Load image
                System.Drawing.Image image = System.Drawing.Image.FromFile(fileName);
                // Check image's width and height
                if (image.Width > width || image.Height > height)
                {
                    image.Dispose();
                    File.Delete(fileName);
                    throw new Exception("Sai kích thước");
                }
                image.Dispose();

                FileInfo fileInfo = new FileInfo(fileName);
                if (fileInfo.Length > 5000000)
                {
                    File.Delete(fileName);
                    throw new Exception("Kích thước file quá lớn.");
                }

                string fileEx = System.IO.Path.GetExtension(fileName).ToLower();


                string[] allowedEx = { ".gif", ".png", ".jpg", ".bmp", ".jpeg" };
                bool fileok = false;

                for (int i = 0; i < allowedEx.Length; i++)
                {

                    if (fileEx == allowedEx[i])
                    {
                        fileok = true;
                    }
                }

                if (fileok == false)
                {
                    throw new Exception("sai ảnh");
                }
            }
            catch (Exception vne)
            {
                throw new Exception("Sai ảnh");
            }

        }

        public static void MoveImage(string fileName, string newFile)
        {
            try
            {
                // Load image
                System.Drawing.Image image = System.Drawing.Image.FromFile(fileName);
                // Save in JPG format
                image.Save(newFile + ".jpg", ImageFormat.Jpeg);
                // Delete temporary file
                image.Dispose();
                File.Delete(fileName);
            }
            catch (FileNotFoundException exc)
            {
                throw new Exception("Sai đường dẫn cũ.");
            }
            catch (OutOfMemoryException exc)
            {
                throw new Exception("Sai hình ảnh nhập vào.");
            }
            catch (ArgumentException exc)
            {
                throw new Exception("Sai đường dẫn mới.");
            }
            catch (ExternalException exc)
            {
                throw new Exception( "Sai hình ảnh xuất ra.");
            }
        }

        public static System.Drawing.Image ResizeImageFile(Stream imageFile, int targetSize)
        {
            System.Drawing.Image original = System.Drawing.Image.FromStream(imageFile);
            int targetH, targetW;
            if (original.Height > original.Width)
            {
                targetH = targetSize;
                targetW = (int)(original.Width * ((float)targetSize / (float)original.Height));
            }
            else
            {
                targetW = targetSize;
                targetH = (int)(original.Height * ((float)targetSize / (float)original.Width));
            }
            System.Drawing.Image imgPhoto = System.Drawing.Image.FromStream(imageFile);
            // Create a new blank canvas.  The resized image will be drawn on this canvas.
            Bitmap bmPhoto = new Bitmap(targetW, targetH, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(72, 72);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
            grPhoto.DrawImage(imgPhoto, new Rectangle(0, 0, targetW, targetH), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel);
            // Save out to memory and then to a file.  We dispose of all objects to make sure the files don't stay locked.
            MemoryStream mm = new MemoryStream();
            bmPhoto.Save(mm, System.Drawing.Imaging.ImageFormat.Jpeg);
            original.Dispose();
            imgPhoto.Dispose();
            bmPhoto.Dispose();
            grPhoto.Dispose();


            return System.Drawing.Image.FromStream(mm); ;
        }


        public static string GetLink (string type, string fileSystem , long userID, long collectionID, long workID, System.Guid pictureID, string extension)
        {
            if ( type.ToLower ( ) == "tranh" )
            {
                return fileSystem + userID + "\\" + pictureID.ToString ( ) + "." + extension;
            }
            else if ( type.ToLower ( ) == "truyen" )
            {
                return fileSystem + collectionID.ToString ( ) + "\\" + workID + "\\" + pictureID.ToString ( ) + "." + extension;
            }
            else if ( type.ToLower ( ) == "biatruyen" )
            {
                return fileSystem + collectionID.ToString ( ) + "\\" + workID + "\\" + "bia." + extension;
            }
            return "";
        }

        public static string getMessage(string messsagewithoutsmile)
        {

            foreach (object source in emotions)
            {
                string[] tmp = (string[])source;
                messsagewithoutsmile = messsagewithoutsmile.Replace(tmp[0], tmp[1]);
            }

            return messsagewithoutsmile;
        }

        private static object[] emotions = new object[]
        {
            new string[] {"]:P","<img alt=\"Nào!!!\" src=\"/smile/emotions/27.gif\"/>"},
            new string[] {":((","<img alt=\"Giời ơi\" src=\"/smile/emotions/6.gif\"/>"},
            new string[] {":))","<img alt=\"hahaaa\" src=\"/smile/emotions/8.gif\"/>"},
            new string[] {":)","<img alt=\"Vui\" src=\"/smile/emotions/13.gif\"/>"},
            new string[] {":(","<img alt=\"Buồn nhỉ\" src=\"/smile/emotions/18.gif\"/>"},
            new string[] {";;)","<img alt=\"Ghét thế\" src=\"/smile/emotions/12.gif\"/>"},
            new string[] {";)","<img alt=\"Hay đấy\" src=\"/smile/emotions/15.gif\"/>"},
            new string[] {"]:D[","<img alt=\"Hồn nhiên\" src=\"/smile/emotions/3.gif\"/>"},
            new string[] {":D","<img alt=\"Duyên nhỉ\" src=\"/smile/emotions/46.gif\"/>"},
            new string[] {":x","<img alt=\"Iu iu\" src=\"/smile/emotions/7.gif\"/>"},
            new string[] {":P","<img alt=\"Eo ôi!\" src=\"/smile/emotions/2.gif\"/>"},
            new string[] {":-O","<img alt=\"Không thể!\" src=\"/smile/emotions/26.gif\"/>"},
            new string[] {"x-(","<img alt=\"Đủ rồi đấy\" src=\"/smile/emotions/45.gif\"/>"},
            new string[] {"X(","<img alt=\"Cái dzì?\" src=\"/smile/emotions/30.gif\"/>"},
            new string[] {":]","<img alt=\"Hay chưa?!\" src=\"/smile/emotions/11.gif\"/>"},
            new string[] {"B-)","<img alt=\"Sành điệu\" src=\"/smile/emotions/38.gif\"/>"},
            new string[] {":-S","<img alt=\"Sợ quá\" src=\"/smile/emotions/35.gif\"/>"},
            new string[] {":|","<img alt=\"Không ý kiến\" src=\"/smile/emotions/1.gif\"/>"},
            new string[] {":-B","<img alt=\"Nghiêm túc nào\" src=\"/smile/emotions/14.gif\"/>"},
            new string[] {"=;","<img alt=\"Khoan đã\" src=\"/smile/emotions/9.gif\"/>"},
            new string[] {"I-)","<img alt=\"Buồn ngủ\" src=\"/smile/emotions/23.gif\"/>"},
            new string[] {":-$","<img alt=\"Suỵt\" src=\"/smile/emotions/17.gif\"/>"},
            new string[] {"8-}","<img alt=\"Ngố\" src=\"/smile/emotions/4.gif\"/>"},
            new string[] {"=P~","<img alt=\"Ngon quá\" src=\"/smile/emotions/20.gif\"/>"},
            new string[] {":-?","<img alt=\"Xét đã\" src=\"/smile/emotions/25.gif\"/>"},
            new string[] {"=D]","<img alt=\"Hoan hô\" src=\"/smile/emotions/28.gif\"/>"},
            new string[] {":-w","<img alt=\"Hãy đợi đấy\" src=\"/smile/emotions/21.gif\"/>"},
            new string[] {"-o[","<img alt=\"Chúa ơi\" src=\"/smile/emotions/19.gif\"/>"},
            new string[] {"-X","<img alt=\"Cẩn thận nhé\" src=\"/smile/emotions/10.gif\"/>"},
            new string[] {":-t","<img alt=\"Khủng khiếp\" src=\"/smile/emotions/22.gif\"/>"},
            new string[] {"!-.-","<img alt=\"Đau lòng quá\" src=\"/smile/emotions/29.gif\"/>"},
            new string[] {"!?","<img alt=\"Chẳng hiểu gì\" src=\"/smile/emotions/33.gif\"/>"},
            new string[] {":017:","<img alt=\"Thường thôi\" src=\"/smile/emotions/34.gif\"/>"},
            new string[] {"]X[","<img alt=\"Kinh khủng\" src=\"/smile/emotions/36.gif\"/>"},
            new string[] {"o^o","<img alt=\"Xí xọn\" src=\"/smile/emotions/40.gif\"/>"},
            new string[] {"!:@","<img alt=\"Thộn!\" src=\"/smile/emotions/42.gif\"/>"},
            new string[] {":-h","<img alt=\"Lêu lêu\" src=\"/smile/emotions/43.gif\"/>"},
            new string[] {"^oo^","<img alt=\"Oaaaaa\" src=\"/smile/emotions/44.gif\"/>"},
            new string[] {":M:","<img alt=\"Ghét thế\" src=\"/smile/emotions/41.gif\"/>"}
        };
    }
       
    }

