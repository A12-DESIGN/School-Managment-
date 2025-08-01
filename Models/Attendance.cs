namespace Crud.Models
{
    public class Attendance
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; }
    }

}
