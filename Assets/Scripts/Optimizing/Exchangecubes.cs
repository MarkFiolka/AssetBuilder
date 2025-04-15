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
            foreach (GameObject obj in new List<GameObject>(ObjectRepository.cubes))
            {
                if (obj != null && obj.name == "cube")
                {
                    // Create six plane objects around the cube.
                    GameObject planeX = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    planeX.name = "plane";
                    GameObject planeY = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    planeY.name = "plane";
                    GameObject planeZ = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    planeZ.name = "plane";
                    GameObject planex = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    planex.name = "plane";
                    GameObject planey = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    planey.name = "plane";
                    GameObject planez = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    planez.name = "plane";
                    
                    planeX.GetComponent<Renderer>().material.color = obj.GetComponent<Renderer>().material.color;
                    planeY.GetComponent<Renderer>().material.color = obj.GetComponent<Renderer>().material.color;
                    planeZ.GetComponent<Renderer>().material.color = obj.GetComponent<Renderer>().material.color;
                    planex.GetComponent<Renderer>().material.color = obj.GetComponent<Renderer>().material.color;
                    planey.GetComponent<Renderer>().material.color = obj.GetComponent<Renderer>().material.color;
                    planez.GetComponent<Renderer>().material.color = obj.GetComponent<Renderer>().material.color;
                    
                    GameObject centerObject = new GameObject("center");
                    centerObject.transform.position = obj.transform.position;
                    centerObject.transform.parent = szeneContent.transform;
                    
                    planeX.transform.parent = centerObject.transform;
                    planeY.transform.parent = centerObject.transform;
                    planeZ.transform.parent = centerObject.transform;
                    planex.transform.parent = centerObject.transform;
                    planey.transform.parent = centerObject.transform;
                    planez.transform.parent = centerObject.transform;
                    
                    float cubePosX = obj.transform.position.x;
                    float cubePosY = obj.transform.position.y;
                    float cubePosZ = obj.transform.position.z;
                    
                    planeX.transform.position = new Vector3(cubePosX + 0.5f, cubePosY, cubePosZ);
                    planex.transform.position = new Vector3(cubePosX - 0.5f, cubePosY, cubePosZ);
                    planeY.transform.position = new Vector3(cubePosX, cubePosY + 0.5f, cubePosZ);
                    planey.transform.position = new Vector3(cubePosX, cubePosY - 0.5f, cubePosZ);
                    planeZ.transform.position = new Vector3(cubePosX, cubePosY, cubePosZ + 0.5f);
                    planez.transform.position = new Vector3(cubePosX, cubePosY, cubePosZ - 0.5f);

                    Vector3 eulerAngles = obj.transform.rotation.eulerAngles;
                    planeX.transform.rotation = Quaternion.Euler(eulerAngles.x - 90, eulerAngles.y, eulerAngles.z - 90);
                    planex.transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z + 90);
                    planeY.transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
                    planey.transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z - 180);
                    planeZ.transform.rotation = Quaternion.Euler(eulerAngles.x - 90, eulerAngles.y, eulerAngles.z + 180);
                    planez.transform.rotation = Quaternion.Euler(eulerAngles.x - 90, eulerAngles.y, eulerAngles.z);
                    
                    planeX.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    planex.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    planeY.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    planey.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    planeZ.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    planez.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    
                    ObjectRepository.cubes.Remove(obj);
                    Destroy(obj);
                    
                    ObjectRepository.planes.Add(planeX);
                    ObjectRepository.planes.Add(planex);
                    ObjectRepository.planes.Add(planeY);
                    ObjectRepository.planes.Add(planey);
                    ObjectRepository.planes.Add(planeZ);
                    ObjectRepository.planes.Add(planez);
                }
            }
        }
    }
}
