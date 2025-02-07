namespace Assets.Scripts.Patterns.StatePattern
{
    public abstract class State<MemberType>
    {
        public StateProvider<MemberType> Provider { get; set; }
        public virtual StateTransition<MemberType> OnEnter(MemberType self) => new(this);
        public virtual StateTransition<MemberType> OnEnter(MemberType self, StateProperties properties) => new(this, properties);
        public virtual void OnExit(MemberType self) { }

        public virtual StateTransition<MemberType> FixedUpdate(MemberType self) => new(this);
        public virtual StateTransition<MemberType> Update(MemberType self) => new(this);
        public virtual StateTransition<MemberType> LateUpdate(MemberType self) => new(this);
    }

    public abstract class State<MemberType, PropertyType> : State<MemberType> where PropertyType : StateProperties
    {
        public override StateTransition<MemberType> OnEnter(MemberType self, StateProperties properties)
        {
            if (properties is PropertyType typedProperties)
                return OnEnter(self, typedProperties);
            return base.OnEnter(self, properties);
        }

        public virtual StateTransition<MemberType> OnEnter(MemberType self, PropertyType properties) => new(this, properties);
    }
}
