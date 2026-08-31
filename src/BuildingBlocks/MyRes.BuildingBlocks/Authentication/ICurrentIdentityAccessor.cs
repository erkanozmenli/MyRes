namespace MyRes.BuildingBlocks.Authentication
{
    public interface ICurrentIdentityAccessor
    {
        IRequestIdentity Identity { get; }
        void SetIdentity(IRequestIdentity identity);
    }
}
