using System.ComponentModel.DataAnnotations;

namespace AntaqaCafe.Api.Dtos.User
{
    public class UpdateUserDto
    {
        [StringLength(100, MinimumLength = 2)]
        public string? FirstName { get; set; }
        
        [StringLength(100, MinimumLength = 2)]
        public string? LastName { get; set; }
        
        [Phone]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }
    }
}