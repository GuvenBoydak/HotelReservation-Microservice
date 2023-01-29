using System.Net;
using System.Net.Mail;
using NotificationService.Api.DTOs;

namespace NotificationService.Api.Services;

public class MailService
{
    public static async Task SendSuccessAsync(PaymentSuccessDto paymentSuccessDto)
    {
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.Credentials = new NetworkCredential("yzl3157test@gmail.com",
            "atlkbjteiruyhcww");
        
        MailMessage mail = new MailMessage("yzl3157test@gmail.com", paymentSuccessDto.ReservationEmail);

        mail.Subject = "Bilgilendirme!!!";
        mail.Body = "Rezervasyonunuz başarılı bir şekilde oluşturuldu.\n \n \n" +
                    $" İsim: {paymentSuccessDto.ReservationName} \n " +
                    $" Konfirmasyon Numarası: {paymentSuccessDto.ConfirmationNumber} \n " +
                    $" Giriş Tarihi: {paymentSuccessDto.ArrivalDate} \n" +
                    $" Çıkış Tarihi: {paymentSuccessDto.DepartureDate} \n " +
                    $" Toplam Tutar: {paymentSuccessDto.TotalAmount}";

        try
        {
            await smtp.SendMailAsync(mail);
        }
        catch (Exception)
        {
            throw new Exception("Message failed to send.");
        }
    }
    
    
    
    public static async Task SendFailedAsync(string receiverMail)
    {
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Port = 587;
        smtp.EnableSsl = true;
        smtp.Credentials = new NetworkCredential("yzl3157test@gmail.com",
            "atlkbjteiruyhcww");
        
        MailMessage mail = new MailMessage("yzl3157test@gmail.com", receiverMail);

        mail.Subject = "Bilgilendirme!!!";
        mail.Body = "Rezervasyon oluşturma esnasinda problem yaşandı. Rezervasyonunuz oluşturulamadı lütfen Kart bilgilerinizi kontrol ederek tekrar deneyiniz.";

        try
        {
            await smtp.SendMailAsync(mail);
        }
        catch (Exception)
        {
            throw new Exception("Message failed to send.");
        }
    }
    
}