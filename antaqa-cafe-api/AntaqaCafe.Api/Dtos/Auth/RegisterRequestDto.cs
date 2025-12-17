using System.ComponentModel.DataAnnotations;

namespace AntaqaCafe.Api.Dtos.Auth
{
    public class RegisterRequestDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
        
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
        
        [Phone]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }
    }
}