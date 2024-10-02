using System.Text.Json.Serialization;

namespace Authentication.API.Models
{
    public class Role
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        [JsonIgnore]
        public ICollection<User> Users { get; set; }
    }
}
