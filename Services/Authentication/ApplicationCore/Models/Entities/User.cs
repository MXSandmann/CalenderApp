using ApplicationCore.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public Role Role { get; private set; }
    }

}
