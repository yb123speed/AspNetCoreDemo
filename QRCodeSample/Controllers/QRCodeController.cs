using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Drawing.Imaging;

namespace QRCodeSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        private IQRCode _qrcode;
        public QRCodeController(IQRCode qrcode)
        {
            _qrcode = qrcode;
        }

        [HttpGet("/api/qrcode")]
        public void GetQRCode(string url, int pixel)
        {
            Response.ContentType="image/jpeg";

            var bitmap= _qrcode.GetQRCode(url, pixel);

            MemoryStream ms =new MemoryStream();
            bitmap.Save(ms,ImageFormat.Jpeg);

            Response.Body.WriteAsync(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length));
            Response.Body.Close();
        }
    }
}
