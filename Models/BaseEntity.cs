using System.Security.Cryptography.X509Certificates;

namespace Crud.Models
{
    public class BaseEntity
    {

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
