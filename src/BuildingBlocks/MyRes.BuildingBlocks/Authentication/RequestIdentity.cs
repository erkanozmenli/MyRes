namespace MyRes.BuildingBlocks.Authentication;

public sealed record RequestIdentity(
    bool IsAuthenticated,
    PrincipalType PrincipalType,
    Guid? UserId,
    string? Username,
    string? Email,
    string? ClientId
) : IRequestIdentity;