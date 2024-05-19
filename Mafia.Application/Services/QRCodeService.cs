using Mafia.Application.Services.Interfaces;
using QRCoder;
using System;

namespace Mafia.Application.Services
{
    //public class QRCodeService : IQRCodeService
    //{
    //    public string GenerateQRCodeToBase64(string text)
    //    {
    //        QRCodeGenerator qrGenerator = new();
    //        QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
    //        PngByteQRCode qrCode = new(qrCodeData);
    //        byte[] graphic = qrCode.GetGraphic(8);
    //        return Convert.ToBase64String(graphic);
    //    }

    //    public byte[] GenerateQRCodeToPng(string text)
    //    {
    //        QRCodeGenerator qrGenerator = new();
    //        QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
    //        PngByteQRCode qrCode = new(qrCodeData);
    //        return qrCode.GetGraphic(8);
    //    }
    //}
}