using System.Collections.Generic;
using UnityEngine;

public static class ObjectRepository
{
    public static List<GameObject> cubes = new List<GameObject>();
    public static List<GameObject> planes = new List<GameObject>();
    public static List<GameObject> miniPlanes = new List<GameObject>();
    public static List<GameObject> selectedCubes = new List<GameObject>();
    public static Dictionary<GameObject, Color> originalCubeColors = new Dictionary<GameObject, Color>();
}