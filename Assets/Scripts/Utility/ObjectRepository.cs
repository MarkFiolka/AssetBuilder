using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class ObjectRepository

    {
        public static readonly List<GameObject> cubes = new List<GameObject>();
        public static readonly List<GameObject> planes = new List<GameObject>();
        public static readonly List<GameObject> miniPlanes = new List<GameObject>();
        public static readonly List<GameObject> selectedCubes = new List<GameObject>();
        public static readonly Dictionary<GameObject, Color> originalCubeColors = new Dictionary<GameObject, Color>();

        public static readonly List<CubeData> savedCubeData = new List<CubeData>();


        public static void RegisterCube(GameObject cube)
        {
            if (!cubes.Contains(cube))
                cubes.Add(cube);
        }

        public static void UnregisterCube(GameObject cube)
        {
            cubes.Remove(cube);
            selectedCubes.Remove(cube);
            originalCubeColors.Remove(cube);
        }

        public static void SaveCubes()
        {
            savedCubeData.Clear();
            foreach (var cube in cubes)
            {
                if (cube != null)
                {
                    Renderer rend = cube.GetComponent<Renderer>();
                    if (rend != null)
                    {
                        savedCubeData.Add(new CubeData
                        {
                            position = cube.transform.position,
                            rotation = cube.transform.rotation,
                            color = rend.material.color
                        });
                    }
                }
            }
        }

    }
}