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
        
        public static void Register(GameObject cube)
        {
            if (!cubes.Contains(cube))
                cubes.Add(cube);
        }

        public static void Unregister(GameObject cube)
        {
            cubes.Remove(cube);
            selectedCubes.Remove(cube);
            originalCubeColors.Remove(cube);
        }
    }
}