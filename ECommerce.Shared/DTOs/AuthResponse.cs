namespace ECommerce.Shared.DTOs
{ // En enkel DTO-klass som används för att skicka tillbaka autentiseringsresultatet från servern till klienten
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty; // JWT-token som klienten kan använda för att autentisera sig i framtida API-anrop
        public bool Success { get; set; } // Indikerar om autentiseringen lyckades eller inte
        public string Message { get; set; } = string.Empty; // Ett meddelande som kan innehålla information om varför autentiseringen misslyckades, eller annan relevant information
    }
}