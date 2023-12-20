using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OrderManagement.Service
{
    public class EmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com"; // Replace with your SMTP server
        private readonly int _smtpPort = 587; // Replace with your SMTP port
        private readonly string _username = "donotreply.ecommerce@gmail.com"; // Replace with your email username
        private readonly string _password = "facatomatfehlcms"; // Replace with your email password
        private readonly string _fromAddress = "donotreply.ecommerce@gmail.com"; // Replace with your email address

        public async Task SendOrderStatusEmailNotification(string recipientEmail, Guid orderId, string newStatus)
        {
            // Configure and send the email notification
            string subject = GenerateCustomSubjectEmailMessage(newStatus);
            string body = GenerateCustomEmailMessage(newStatus, orderId);

            await SendEmail(recipientEmail, subject, body);
        }

        private string GenerateCustomSubjectEmailMessage(string newStatus)
        {
            string subjectMsg = "";
            switch(newStatus.ToLower())
            {
                case "ordered":
                    subjectMsg = "Order Confirmation - Thank You for Your Purchase!";
                    break;
                case "shipped":
                    subjectMsg = "Your Order is on its Way!";
                    break;
                case "delivered":
                    subjectMsg = "Delivery Confirmation - Thank You!";
                    break;
                case "cancelled":
                    subjectMsg = "Order Cancellation Confirmation";
                    break;
                default:
                    subjectMsg = "Return Request Acknowledgement";
                    break;
            }
            return subjectMsg;
        }
        private string GenerateCustomEmailMessage(string newStatus, Guid orderId)
        {
            string bodyMsg = "";
            switch (newStatus.ToLower())
            {
                case "ordered":
                    bodyMsg = $"Hello Dear Customer,\r\n\r\nThank you for shopping with us! Your order [{orderId}] has been successfully placed. We're now processing it and will notify you once it's shipped.\r\n\r\nFor any queries or assistance, feel free to reach out to our support team.\r\n\r\n\r\nBest regards,\r\n[Actualize]";
                    break;
                case "shipped":
                    bodyMsg = $"Hello Dear Customer,\r\n\r\nGreat news! Your order [{orderId}] has been shipped and is on its way to you. You can track your shipment using the tracking ID provided.\r\n\r\nWe hope you enjoy your purchase! For any assistance, contact our support team.\r\n\r\n\r\nBest regards,\r\n[Actualize]";
                    break;
                case "delivered":
                    bodyMsg = $"Hello Dear Customer,\r\n\r\nCongratulations! Your order [{orderId}] has been successfully delivered. We hope you love your purchase and look forward to serving you again.\r\n\r\nShould you have any feedback or need assistance, don't hesitate to contact us.\r\n\r\n\r\nBest regards,\r\n[Actualize]";
                    break;
                case "cancelled":
                    bodyMsg = $"Hello Dear Customer,\r\n\r\nWe regret to inform you that your order [{orderId}] has been cancelled as per your request. If you have any concerns, please reach out to us.\r\n\r\nThank you for choosing us. We look forward to serving you in the future.\r\n\r\n\r\nBest regards,\r\n[Actualize]";
                    break;
                default:
                    bodyMsg = $"Hello Dear Customer,\r\n\r\nWe've received your return request for order [{orderId}]. Our team will review it shortly and get back to you with the return process details.\r\n\r\nThank you for your cooperation. If you have any questions, feel free to contact us.\r\n\r\n\r\nBest regards,\r\n[Actualize]";
                    break;
            }
            return bodyMsg;
        }

        private async Task SendEmail(string recipientEmail, string subject, string body)
        {
            // Configure your SMTP settings
            using (SmtpClient smtpClient = new SmtpClient(_smtpServer, _smtpPort))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_username, _password);
                smtpClient.EnableSsl = true;

                // Create the email message
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(_fromAddress);
                    mail.To.Add(recipientEmail);
                    mail.Subject = subject;
                    mail.Body = body;

                    // Send the email
                    await smtpClient.SendMailAsync(mail);
                }
            }
        }
    }
}

