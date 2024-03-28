using SendEmail.Service;

namespace SendEmail
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();
            builder.Services.AddTransient<IMailService, MailService>();
            var host = builder.Build();
            host.Run();
        }
    }
}