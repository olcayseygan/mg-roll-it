using UnityEngine;

namespace Assets.Scripts.Patterns.StatePattern.Plugins.StateAnimator
{

    public interface IHasStateAnimator<MemberType> where MemberType : MonoBehaviour
    {
        public StateAnimatorHandler<MemberType> StateAnimatorHandler { get; }
    }
}
