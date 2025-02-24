namespace Crowbond.Modules.Users.Infrastructure.Identity;

public class CognitoOptions
{
    public string UserPoolId { get; set; }
    public string UserPoolClientId { get; set; }
    public string UserPoolClientSecret { get; set; }
    public string Region { get; set; }
}
