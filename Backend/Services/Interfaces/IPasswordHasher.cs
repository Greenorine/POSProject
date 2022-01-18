namespace POSProject.Backend.Services.Interfaces;

public interface IPasswordHasher
{
    public string HashPassword(string password);

    public bool VerifyHashedPassword(string hashedPassword, string password);
}