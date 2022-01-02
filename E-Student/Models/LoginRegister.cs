namespace E_Student.Models;

public class LoginRegisterModels
{
    public string? Email { get; set; }
    public string? Password_Hash { get; set; }
    public string? Role { get; set; }
    public string? Salt { get; set; }
}