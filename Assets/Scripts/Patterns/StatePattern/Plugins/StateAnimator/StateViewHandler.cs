using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Patterns.StatePattern.Plugins
{
  public class StateViewHandler<MemberType> where MemberType : MonoBehaviour
  {
    private readonly List<StateViewPanel> _panels = new();

    public void RegisterStateViewPanel(StateViewPanel panel)
    {
      _panels.Add(panel);
    }

    public void SwitchTo<T2>() where T2 : StateViewPanel
    {
      foreach (var panel in _panels)
      {
        if (panel.GetType() == typeof(T2))
        {
          panel.Show();
        }
        else if (panel.gameObject.activeSelf)
        {
          panel.Hide();
        }
      }
    }
  }
}
