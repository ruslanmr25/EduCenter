using Application.Abstracts;

namespace Application.Services;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string hashedPassword)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        catch (BCrypt.Net.SaltParseException)
        {
            // log qiling: buzilgan hash
            return false;
        }
        catch (Exception)
        {
            // kutilmagan xatoliklar — hamma joyda tutib olinish kerak
            return false;
        }
    }
}
