using UnityEngine;

namespace Utility
{
    public class CubeRegistration : MonoBehaviour
    {
        void Awake()
        {
            ObjectRepository.Register(gameObject);
        }

        void OnDestroy()
        {
            ObjectRepository.Unregister(gameObject);
        }
    }
}