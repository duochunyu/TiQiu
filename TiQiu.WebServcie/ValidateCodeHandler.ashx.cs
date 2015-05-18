using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using TiQiu.Biz;
using System.Web.SessionState;
using TiQiu.Model;
namespace TiQiu.WebServcie
{
    /// <summary>
    /// ValidateCodeHandler 的摘要说明
    /// </summary>

    public class ValidateCodeHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            var verifyType = Util.GetParamter<string>(context.Request, "CodeFlag", false) ?? "ValidateCode";
            var code = GenerateCheckCode().ToUpper();
           // CookieUtil.SetCookie(verifyType, code);
            SessionUtil.SetSession("ValidateCode", code);
            CreateCheckCodeImage(code, context.Response);
        }

        private static string GenerateCheckCode()
        {
            char code;
            var checkCode = String.Empty;

            var random = new Random();

            for (var i = 0; i < 5; i++)
            {
                var number = random.Next();

                if (number % 2 == 0)
                    code = (char)('1' + (char)(number % 9));
                else
                    code = (char)('A' + (char)(number % 26));
                checkCode += code.ToString();
            }
            return checkCode;
        }

        private static void CreateCheckCodeImage(string checkCode, HttpResponse response)
        {
            if (checkCode == null || checkCode.Trim().Length <= 0)
            {
                return;
            }

            Bitmap image = null;
            Graphics g = null;
            try
            {
                image = new Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22);
                g = Graphics.FromImage(image);
                //生成随机生成器

                var random = new Random();

                //清空图片背景色

                g.Clear(Color.White);

                //画图片的背景噪音线

                for (var i = 0; i < 10; i++)
                {
                    var x1 = random.Next(image.Width);
                    var x2 = random.Next(image.Width);
                    var y1 = random.Next(image.Height);
                    var y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                var font = new Font("Arial", 13, (FontStyle.Bold));
                var brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Black, Color.Gray, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 1);

                //画图片的前景噪音点

                for (var i = 0; i < 20; i++)
                {
                    var x = random.Next(image.Width);
                    var y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(0x8b, 0x8b, 0x8b));
                }

                //画图片的边框线

                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                var ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                response.ClearContent();
                response.ContentType = "image/Gif";
                response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                if (g != null)
                {
                    g.Dispose();
                }
                if (image != null)
                {
                    image.Dispose();
                }
            }
        }

        #region IHttpHandler Members

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #endregion
    }
}
