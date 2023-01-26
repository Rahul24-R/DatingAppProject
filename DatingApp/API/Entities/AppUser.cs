using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class AppUser
    {
        [Key]                       //marking ID as primary key in the DB column
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        
    }
}