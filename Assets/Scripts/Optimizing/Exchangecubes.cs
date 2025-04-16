using System.Collections.Generic;
using UnityEngine;

public class Exchangecubes : MonoBehaviour
{
    private GameObject szeneContent;

    void Awake()
    {
        szeneContent = GameObject.Find("SzeneContainer");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ProcessCubeExchange();
        }
    }

    private void ProcessCubeExchange()
    {
        foreach (GameObject cube in new List<GameObject>(ObjectRepository.cubes))
        {
            if (cube != null && cube.name == "cube")
            {
                ExchangeCube(cube);
            }
        }
    }

    private void ExchangeCube(GameObject cube)
    {
        GameObject centerObject = CreateCenterObject(cube);
        Vector3 cubePos = cube.transform.position;
        Vector3 cubeEuler = cube.transform.rotation.eulerAngles;

        GameObject planeX = CreatePlane(cube, centerObject.transform);
        GameObject planex = CreatePlane(cube, centerObject.transform);
        GameObject planeY = CreatePlane(cube, centerObject.transform);
        GameObject planey = CreatePlane(cube, centerObject.transform);
        GameObject planeZ = CreatePlane(cube, centerObject.transform);
        GameObject planez = CreatePlane(cube, centerObject.transform);

        planeX.transform.position = new Vector3(cubePos.x + 0.5f, cubePos.y, cubePos.z);
        planex.transform.position = new Vector3(cubePos.x - 0.5f, cubePos.y, cubePos.z);
        planeY.transform.position = new Vector3(cubePos.x, cubePos.y + 0.5f, cubePos.z);
        planey.transform.position = new Vector3(cubePos.x, cubePos.y - 0.5f, cubePos.z);
        planeZ.transform.position = new Vector3(cubePos.x, cubePos.y, cubePos.z + 0.5f);
        planez.transform.position = new Vector3(cubePos.x, cubePos.y, cubePos.z - 0.5f);

        planeX.transform.rotation = Quaternion.Euler(cubeEuler.x - 90, cubeEuler.y, cubeEuler.z - 90);
        planex.transform.rotation = Quaternion.Euler(cubeEuler.x, cubeEuler.y, cubeEuler.z + 90);
        planeY.transform.rotation = Quaternion.Euler(cubeEuler.x, cubeEuler.y, cubeEuler.z);
        planey.transform.rotation = Quaternion.Euler(cubeEuler.x, cubeEuler.y, cubeEuler.z - 180);
        planeZ.transform.rotation = Quaternion.Euler(cubeEuler.x - 90, cubeEuler.y, cubeEuler.z + 180);
        planez.transform.rotation = Quaternion.Euler(cubeEuler.x - 90, cubeEuler.y, cubeEuler.z);

        ObjectRepository.cubes.Remove(cube);
        Destroy(cube);

        ObjectRepository.planes.Add(planeX);
        ObjectRepository.planes.Add(planex);
        ObjectRepository.planes.Add(planeY);
        ObjectRepository.planes.Add(planey);
        ObjectRepository.planes.Add(planeZ);
        ObjectRepository.planes.Add(planez);
    }

    private GameObject CreateCenterObject(GameObject cube)
    {
        GameObject centerObject = new GameObject("center");
        centerObject.transform.position = cube.transform.position;
        centerObject.transform.parent = szeneContent.transform;
        return centerObject;
    }

    private GameObject CreatePlane(GameObject cube, Transform parent)
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.name = "plane";
        plane.GetComponent<Renderer>().material.color = cube.GetComponent<Renderer>().material.color;
        plane.transform.parent = parent;
        plane.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        return plane;
    }
}
