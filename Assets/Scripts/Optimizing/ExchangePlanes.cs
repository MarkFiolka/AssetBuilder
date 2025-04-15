using System.Collections.Generic;
using UnityEngine;

public class ExchangePlanes : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            foreach (GameObject obj in new List<GameObject>(ObjectRepository.planes))
            {
                if (obj != null && obj.name == "plane")
                {
                    GameObject plane1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    plane1.name = "miniPlane";
                    GameObject plane2 = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    plane2.name = "miniPlane";
                    GameObject plane3 = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    plane3.name = "miniPlane";
                    GameObject plane4 = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    plane4.name = "miniPlane";
                    
                    plane1.GetComponent<Renderer>().material.color = obj.GetComponent<Renderer>().material.color;
                    plane2.GetComponent<Renderer>().material.color = obj.GetComponent<Renderer>().material.color;
                    plane3.GetComponent<Renderer>().material.color = obj.GetComponent<Renderer>().material.color;
                    plane4.GetComponent<Renderer>().material.color = obj.GetComponent<Renderer>().material.color;
                    
                    GameObject centerObject = new GameObject("planeSide");
                    centerObject.transform.position = obj.transform.position;
                    centerObject.transform.parent = obj.transform.parent;
                    
                    plane1.transform.parent = centerObject.transform;
                    plane2.transform.parent = centerObject.transform;
                    plane3.transform.parent = centerObject.transform;
                    plane4.transform.parent = centerObject.transform;

                    Vector3 planePosition = obj.transform.position;
                    Vector3 forwardOffset = obj.transform.forward * 0.25f;
                    Vector3 rightOffset = obj.transform.right * 0.25f;

                    plane1.transform.position = planePosition + forwardOffset + rightOffset;
                    plane2.transform.position = planePosition + forwardOffset - rightOffset;
                    plane3.transform.position = planePosition - forwardOffset + rightOffset;
                    plane4.transform.position = planePosition - forwardOffset - rightOffset;

                    plane1.transform.rotation = obj.transform.rotation;
                    plane2.transform.rotation = obj.transform.rotation;
                    plane3.transform.rotation = obj.transform.rotation;
                    plane4.transform.rotation = obj.transform.rotation;

                    Vector3 scale = new Vector3(0.05f, 0.05f, 0.05f);
                    plane1.transform.localScale = scale;
                    plane2.transform.localScale = scale;
                    plane3.transform.localScale = scale;
                    plane4.transform.localScale = scale;

                    ObjectRepository.planes.Remove(obj);
                    Destroy(obj);
                    
                    ObjectRepository.miniPlanes.Add(plane1);
                    ObjectRepository.miniPlanes.Add(plane2);
                    ObjectRepository.miniPlanes.Add(plane3);
                    ObjectRepository.miniPlanes.Add(plane4);
                }
            }
        }
    }
}
