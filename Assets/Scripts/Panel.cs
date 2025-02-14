using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] private GameObject _panelGameObject;

        public void Show()
        {
            _panelGameObject.SetActive(true);
        }

        public void Hide()
        {
            _panelGameObject.SetActive(false);
        }
    }
}