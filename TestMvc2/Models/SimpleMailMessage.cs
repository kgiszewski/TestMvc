namespace TestMvc2.Models
{
    public class SimpleMailMessage
    {
        public string To;
        public string From;
        public string Cc;
        public string Bcc;
        public string Subject;
        public string Body;
        public bool IsHtml = true;
    }
}