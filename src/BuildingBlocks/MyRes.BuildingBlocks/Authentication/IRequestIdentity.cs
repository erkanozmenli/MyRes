namespace MyRes.BuildingBlocks.Authentication
{
    public interface IRequestIdentity
    {
        bool IsAuthenticated { get; }
        PrincipalType PrincipalType { get; }
        Guid? UserId { get; }
        string? Username { get; }
        string? Email { get; }
        string? ClientId { get; }
    }
}
