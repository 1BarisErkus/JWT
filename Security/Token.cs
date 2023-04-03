namespace JWT_TanerSaydam.Security
{
    public class Token
    {
        public string AccessToken { get; set; }

        // Uygulamayı kullanan kullanıcının süresi bitmişse kullanıcı istek attığında işlemi kesmeden refresh token sayesinde token'ı yenileriz
        // Refresh tokenlar access tokenlardan uzun olur
        public string RefreshToken { get; set; } 
        public DateTime Expiration { get; set; }
    }
}
