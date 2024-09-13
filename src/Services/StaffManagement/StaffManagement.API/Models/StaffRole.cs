using System.Text.Json.Serialization;

namespace StaffManagement.API.Models
{
    public class StaffRole
    {
        public Guid StaffRoleId { get; set; }
        public string StaffRoleName { get; set; }
        //Navigation properties
        [JsonIgnore]
        public ICollection<Staff> Staffs { get; set; }
    }
}
