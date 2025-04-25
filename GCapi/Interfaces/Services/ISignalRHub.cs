namespace gcapi.Interfaces.Services
{
    public interface ISignalRHub
    {
        public Task SendMessageToAll(string message);
    }
}
