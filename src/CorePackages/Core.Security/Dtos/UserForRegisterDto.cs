namespace Core.Security.Dtos;

public class UserForRegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string NameSurname { get; set; }
    public string Username { get; set; }
}