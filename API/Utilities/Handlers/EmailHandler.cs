using System.Net.Mail;
using API.Contracts;

namespace API.Utilities.Handlers;

public class EmailHandler : IEmailHandler
{
    private readonly string _fromEmailAdress;
    private readonly int _port;
    private readonly string _server;

    public EmailHandler(string server, int port,
        string fromEmailAdress) // Parameter yang dibutuhkan untuk mengirim email
    {
        _server = server; // Server email
        _port = port; // Port email
        _fromEmailAdress = fromEmailAdress; // Email pengirim
    }

    // Method untuk mengirim email
    public void Send(string subject, string body, string toEmail)
    {
        var message = new MailMessage() // Membuat objek MailMessage
        {
            From = new MailAddress(_fromEmailAdress), // Email pengirim
            Subject = subject, // Subject email
            Body = body, // Body email
            IsBodyHtml = true // Body email berupa HTML
        };

        message.To.Add(new MailAddress(toEmail)); // Untuk mngirim email ke email tujuan

        using var smtpClient = new SmtpClient(_server, _port); // Membuat objek SmtpClient
        smtpClient.Send(message); // Mengirim email
    }
}