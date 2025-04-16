using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class SpawnCubes : MonoBehaviour
    {
        private List<Vector3> cubePositions = new List<Vector3>();

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKey(KeyCode.KeypadPeriod))
            {
                IntBlock();
            }
        }

        private void IntBlock()
        {
            int randomX = Mathf.RoundToInt(Random.Range(-10, 10));
            int randomY = Mathf.RoundToInt(Random.Range(-10, 10));
            int randomZ = Mathf.RoundToInt(Random.Range(-10, 10));

            Vector3 randomPos = new Vector3(randomX, randomY, randomZ);

            if (!cubePositions.Contains(randomPos))
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = randomPos;
                cube.name = "cube";

                MeshRenderer mr = cube.GetComponent<MeshRenderer>();
                mr.material.color = Settings.Instance.color;

                GameObject sceneContainer = GameObject.Find("SzeneContainer");
                if (sceneContainer != null)
                {
                    cube.transform.parent = sceneContainer.transform;
                }
                else
                {
                    Logger.LogError("GameObject 'SzeneContainer' not found!");
                }

                cubePositions.Add(randomPos);
                ObjectRepository.cubes.Add(cube);
            }
            else
            {
                Logger.Log("Position is taken: " + randomPos);
            }
        }
    }
}