using UnityEngine;

namespace Assets.Scripts.Patterns.SingletonPattern
{
    public class SingletonProvider<MemberType> : MonoBehaviour where MemberType : MonoBehaviour
    {
        private static MemberType _instance;

        public static MemberType I
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<MemberType>();
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this as MemberType;
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }

    public class GlobalSingletonProvider<MemberType> where MemberType : new()
    {
        private static MemberType _instance;
        public static MemberType Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MemberType();
                }

                return _instance;
            }
        }
    }
}
