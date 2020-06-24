using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ZXing;
using ZXing.QrCode;

namespace DotNetFramework.Common.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class QRCode
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="content">需要生成的内容</param>
        /// <param name="path">需要保存的路径</param>
        /// <param name="size">需要生成的size</param>
        public static void EnQRCode(string content, string path, int size = 256)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options.DisableECI = true;
            //设置内容编码
            options.CharacterSet = "UTF-8";
            //设置二维码的宽度和高度
            options.Width = size;
            options.Height = size;
            //设置二维码的边距,单位不是固定像素
            options.Margin = 1;
            writer.Options = options;

            Bitmap map = writer.Write(content);

            map.Save(path, ImageFormat.Png);
            map.Dispose();
        }

        public static Bitmap EnQRCode(string content, int size = 256)
        {
            BarcodeWriter writer = new BarcodeWriter();

            writer.Format = BarcodeFormat.QR_CODE;

            QrCodeEncodingOptions options = new QrCodeEncodingOptions();

            options.DisableECI = true;

            //设置内容编码
            options.CharacterSet = "UTF-8";

            //设置二维码的宽度和高度
            options.Width = size;

            options.Height = size;

            //设置二维码的边距,单位不是固定像素
            options.Margin = 1;

            writer.Options = options;

            Bitmap map = writer.Write(content);

            return map;
        }



        /// <summary>
        /// 解析二维码
        /// </summary>
        /// <param name="path">需要解析的路径</param>
        /// <returns></returns>
        public static string DeQRCode(string path)
        {
            string str = "";
            Result result = null;
            BarcodeReader reader = new BarcodeReader();
            reader.Options.CharacterSet = "UTF-8";
            using (Bitmap bmp = new Bitmap(path))
            {
                result = reader.Decode(bmp);
            }
            if (result != null)
                str = result.Text;

            return str;
        }

        public static string DeQRCode(Stream imgStream)
        {
            string str = "";
            Result result = null;
            BarcodeReader reader = new BarcodeReader();
            reader.Options.CharacterSet = "UTF-8";
            using (Bitmap bmp = new Bitmap(imgStream))
            {
                result = reader.Decode(bmp);
            }
            if (result != null)
                str = result.Text;

            return str;
        }

    }
}
