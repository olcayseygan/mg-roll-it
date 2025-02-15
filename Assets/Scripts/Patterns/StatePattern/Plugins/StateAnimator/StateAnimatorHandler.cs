using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Patterns.StatePattern.Plugins.StateAnimator
{

  public class StateAnimatorHandler<MemberType> where MemberType : MonoBehaviour
  {
    private readonly Animator _animator;
    private readonly List<string> _states = new();

    public StateAnimatorHandler(Animator animator)
    {
      if (animator == null)
      {
        throw new System.ArgumentNullException(nameof(animator));
      }
      _animator = animator;
    }

    public void RegisterStates(List<State<MemberType>> states)
    {
      _states.Clear();
      foreach (var state in states)
      {
        _states.Add(state.GetType().Name);
      }
    }

    public void SwitchTo<T2>() where T2 : State<MemberType>
    {
      if (_animator == null)
      {
        return;
      }

      foreach (var state in _states)
      {
        _animator.SetBool(state, false);
      }

      _animator.SetBool(typeof(T2).Name, true);
    }
  }
}
