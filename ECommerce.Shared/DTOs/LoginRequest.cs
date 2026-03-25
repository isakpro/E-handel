using System.ComponentModel.DataAnnotations;
// En enkel DTO-klass som används för att skicka inloggningsdata från klienten till servern
namespace ECommerce.Shared.DTOs
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "E-post är obligatoriskt.")]
        [EmailAddress(ErrorMessage = "Ogiltig e-postadress.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lösenord är obligatoriskt.")]
        [MinLength(6, ErrorMessage = "Lösenordet måste vara minst 6 tecken.")]
        public string Password { get; set; } = string.Empty;
    }
}