using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OceanStore.BusinessLayer.Helpers
{
    public static class Helper
    {
        public enum Roles
        {
            SuperAdmin,
            Admin,
            Member
        }

        public static bool CheckActive(bool active)
        {
            return active ? false : true;
        }

        public static async Task SendMailAsync(string messageSubject, string messageBody, string mailTo)
        {
            SmtpClient client = new SmtpClient("smtp.yandex.com", 587);
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("Farid.g@itbrains.edu.az", "icjdfiehjeibwwpp");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage message = new MailMessage("Farid.g@itbrains.edu.az", mailTo);
            message.Subject = messageSubject;
            message.Body = messageBody;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            await client.SendMailAsync(message);
        }

        #region Image
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }
        public static bool IsOlderTwoMB(this IFormFile file)
        {
            return file.Length > 2 * 1024 * 1024;
        }
        public static async Task<string> SaveFileAsync(this IFormFile file, string folder)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string fullPath = Path.Combine(folder, fileName);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
        #endregion
    }
}
