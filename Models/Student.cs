
 using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Crud.Models


{
    public class Student : BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault] // important to let Mongo auto-generate
        public string? Id { get; set; }  // nullable string

        public string Name { get; set; } = "";
        public int Age { get; set; }     // must match JSON number
        public string ClassId { get; set; } = ""; // not "int"
        public string ParentContact {  get; set; }
    }
}
