namespace US.IService.ShortUrl.DTOs
{
    public class ShortUrlResponseDto
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public string ShortURL { get; set; }
    }
}
