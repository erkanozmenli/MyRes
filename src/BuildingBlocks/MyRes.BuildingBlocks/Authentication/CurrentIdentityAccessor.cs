namespace MyRes.BuildingBlocks.Authentication
{
    public sealed class CurrentIdentityAccessor : ICurrentIdentityAccessor
    {
        public IRequestIdentity Identity { get; private set; } = new RequestIdentity(
                                                                        IsAuthenticated: false,
                                                                        PrincipalType: PrincipalType.Anonymous,
                                                                        UserId: null,
                                                                        Username: null,
                                                                        Email: null,
                                                                        ClientId: null);

        public void SetIdentity(IRequestIdentity identity)
        {
            Identity = identity;
        }

    }
}
