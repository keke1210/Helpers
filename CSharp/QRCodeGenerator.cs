using ZXing;
using ZXing.Common;
using System.IO;
using System.Drawing.Imaging;

/// <summary>
/// Summary description for QRCodeGenerator
/// </summary>
public static class QRCodeGenerator
{
    public static void GenerateQRPNG(string valueToQR, string exportPath, int height, int width, int margin = 1)
    {
        var qrcode = new QRCodeWriter();
        var qrValue = valueToQR;

        var barcodeWriter = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new EncodingOptions
            {
                Height = height,
                Width = width,
                Margin = margin
            }
        };

        using (var bitmap = barcodeWriter.Write(qrValue))
        //using (var stream = new MemoryStream())
        {
            bitmap.Save(exportPath, System.Drawing.Imaging.ImageFormat.Png);
        }
    }

    public static string GenerateBase64QR(string valueToQR, int height, int width, int margin = 1)
    {
        string base64img = "";

        var qrValue = valueToQR;
        var barcodeWriter = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new EncodingOptions
            {
                Height = height,
                Width = width,
                Margin = margin
            }
        };

        using (var bitmap = barcodeWriter.Write(qrValue))
        using (var stream = new MemoryStream())
        {
            bitmap.Save(stream, ImageFormat.Gif);

            base64img = String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(stream.ToArray()));
        }

        return base64img;
    }

}