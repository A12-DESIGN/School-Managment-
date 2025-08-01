namespace Crud.Models
{
    public class Notifications
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TargetAudience { get; set; } // All, Students, Teachers
    }


}
