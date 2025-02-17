using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Patterns.FactoryPattern
{
    public class FactoryProvider<MemberType> where MemberType : MonoBehaviour
    {
        public List<MemberType> Instances { get; } = new();

        public MemberType Create(GameObject prefab)
        {
            MemberType instance = Object.Instantiate(prefab).GetComponent<MemberType>();
            Instances.Add(instance);
            return instance;
        }

        public void Dismantle(MemberType instance, float delay = 0)
        {
            Instances.Remove(instance);
            if (delay == 0)
            {
                Object.Destroy(instance.gameObject);
            }
            else
            {
                Object.Destroy(instance.gameObject, delay);
            }
        }
    }
}
