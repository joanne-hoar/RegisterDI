namespace RegisterDI;

public class EmailSender : IEmailSender
{
    public string SendMail(string username)
    {
        return $"[REAL] Welcome, {username}! An email has been sent.";
    }
}
