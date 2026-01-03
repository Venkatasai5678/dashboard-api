using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.MODEL
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }   // ✅ PK + Identity

        [Column(TypeName = "varchar(20)")]
        public string? Gmail { get; set; }   // ❌ NOT PK

        [Column("Fake_name", TypeName = "varchar(200)")]
        public string? fakename { get; set; }

        public string? Name { get; set; }
        public string? MobNo { get; set; }
        public int CityId { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }


}
