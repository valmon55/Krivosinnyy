namespace FKA.Krivosinnyy.Services.IServices
{
    public interface IEmailSender
    {
        public void Sent(string email, string subject, string message);
    }
}
