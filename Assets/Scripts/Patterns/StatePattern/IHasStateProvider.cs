namespace Assets.Scripts.Patterns.StatePattern
{
    public interface IHasStateProvider<MemberType>
    {
        public StateProvider<MemberType> StateProvider { get; }
    }
}
