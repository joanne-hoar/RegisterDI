namespace RegisterDI;

public class EmailSender
{
    public string SendMail(string username)
    {
        return $"{username}, we sent you mail!";
    }
}
