namespace TokenBlackListService;
public class TokenBlacklist
{
    private readonly HashSet<string> revokedTokens = new HashSet<string>();

    public bool IsTokenRevoked(string token)
    {
        return revokedTokens.Contains(token);
    }

    public void RevokeToken(string token)
    {
        revokedTokens.Add(token);
    }
}