namespace RegisterDI;

public class MockEmailSender : IEmailSender
{
    public string SendMail(string username)
    {
        // Simulate a mock email sender for testing or development
        return $"[MOCK] Welcome, {username}! (No real email sent.)";
    }
}
