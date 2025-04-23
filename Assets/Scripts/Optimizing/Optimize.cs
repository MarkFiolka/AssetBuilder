using UnityEngine;
using Utility;

namespace Optimizing
{
    public class Optimize : MonoBehaviour
    {
        public static Optimize Instance { get; private set; }

        ExchangeCubes exchangeCubes;
        ExchangePlanes exchangePlanes;
        RemoveInterior removeInterior;

        void Awake()
        {
            Instance = this;

            exchangeCubes = FindObjectOfType<ExchangeCubes>();
            exchangePlanes = FindObjectOfType<ExchangePlanes>();
            removeInterior = FindObjectOfType<RemoveInterior>();
        }

        public static void StartOptimization()
        {
            ObjectRepository.SaveCubes();
            Instance.exchangeCubes.RunOptimizationProcessCubeExchange();
            Instance.exchangePlanes.RunOptimizationPlaneExchange();
            Instance.removeInterior.RunOptimizationRemoveInteriorPlanesWithinMesh();
        }

        public static void RevertOptimization()
        {
            GameObject szeneContainer = GameObject.Find("SzeneContainer");
            
            RemoveChildsFromContainer();
            
            foreach (var data in ObjectRepository.savedCubeData)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = data.position;
                cube.transform.rotation = data.rotation;
                cube.tag = "cube";
                cube.name = "cube";
                
                cube.transform.parent = szeneContainer.transform;

                Renderer renderer = cube.GetComponent<Renderer>();
                renderer.material.color = data.color;

                cube.AddComponent<CubeRegistration>();
            }
        }

        private static void RemoveChildsFromContainer()
        {
            GameObject szeneContainer = GameObject.Find("SzeneContainer");

            if (szeneContainer != null)
            {
                foreach (Transform child in szeneContainer.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}