using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{

    [CreateAssetMenu(fileName = "Scriptable Object", menuName = "ScriptableObjects/Skin Data", order = 1)]
    public class SkinDataSO : ScriptableObject
    {
        public new string name;
        public GameObject prefab;
    }
}