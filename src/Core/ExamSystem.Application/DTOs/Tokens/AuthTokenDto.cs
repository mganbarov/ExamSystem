namespace ExamSystem.Application.DTOs.Tokens
{
    public record AuthTokenDto(string Token, DateTime ExpirationUtc);
    
}
