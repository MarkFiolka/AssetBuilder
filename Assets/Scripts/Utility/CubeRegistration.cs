using UnityEngine;

namespace Utility
{
    public class CubeRegistration : MonoBehaviour
    {
        void Awake()
        {
            ObjectRepository.RegisterCube(gameObject);
        }

        void OnDestroy()
        {
            ObjectRepository.UnregisterCube(gameObject);
        }
    }
}