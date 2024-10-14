namespace gcapi.Interfaces
{
    public interface IAuthService
    {
        public Task AuthorizeUser();
        public Task RegisterUser();
    }
}
