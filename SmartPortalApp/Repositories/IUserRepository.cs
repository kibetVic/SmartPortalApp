namespace SmartPortalApp.Repositories
{
    public interface IUserRepository
    {
        bool ValidateLastChanged(string lastChanged);
    }
}

