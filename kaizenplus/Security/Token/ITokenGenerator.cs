using kaizenplus.Attributes;
using kaizenplus.Security.Token.Models;

namespace kaizenplus.Security.Token
{
    [ScopedInjectable]
    public interface ITokenGenerator
    {
        string Generate(GenerateTokenInput user);

        RefreshTokenOutput GenerateRefreshToken();
    }
}