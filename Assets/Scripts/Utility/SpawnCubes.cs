// File: SpawnCubes.cs
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Utility
{
    public class SpawnCubes : MonoBehaviour
    {
        private readonly List<Vector3> cubePositions = new List<Vector3>();

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKey(KeyCode.KeypadPeriod))
                IntBlock();
        }

        private void IntBlock()
        {
            int x = Mathf.RoundToInt(Random.Range(-10f, 10f));
            int y = Mathf.RoundToInt(Random.Range(-10f, 10f));
            int z = Mathf.RoundToInt(Random.Range(-10f, 10f));
            var pos = new Vector3(x, y, z);

            if (cubePositions.Contains(pos))
            {
                Logger.Log("Position is taken: " + pos);
                return;
            }

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = pos;
            cube.tag = "cube";
            cube.name = "cube";
            cube.AddComponent<CubeRegistration>();

            var mr = cube.GetComponent<MeshRenderer>();
            mr.material.color = Settings.Instance.color;

            var container = GameObject.Find("SzeneContainer");
            if (container) cube.transform.SetParent(container.transform, true);
            else Logger.LogError("GameObject 'SzeneContainer' not found!");

            cubePositions.Add(pos);
        }
    }
}