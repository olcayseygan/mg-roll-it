using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Patterns.StatePattern.Plugins
{
  public class StateViewPanel : MonoBehaviour
  {
    public virtual void Show()
    {
      gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
      gameObject.SetActive(false);
    }
  }
}
