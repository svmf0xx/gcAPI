namespace gcapi.Dto
{
    public class ResponseRegisterDto
    {
        public string Username { get; set; } = string.Empty;
        public string UrlToken { get; set; } = string.Empty;
        public bool isUsernameExist { get; set; } = false;
    }
}
