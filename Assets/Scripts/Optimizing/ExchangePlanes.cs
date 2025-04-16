using System.Collections.Generic;
using UnityEngine;

public class ExchangePlanes : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ProcessPlaneExchange();
        }
    }

    private void ProcessPlaneExchange()
    {
        foreach (GameObject obj in new List<GameObject>(ObjectRepository.planes))
        {
            if (obj != null && obj.name == "plane")
            {
                ExchangePlane(obj);
            }
        }
    }

    private void ExchangePlane(GameObject original)
    {
        GameObject centerObject = CreateCenterObject(original);

        Vector3 planePosition = original.transform.position;
        Vector3 forwardOffset = original.transform.forward * 0.25f;
        Vector3 rightOffset = original.transform.right * 0.25f;

        GameObject miniPlane1 = CreateMiniPlane(original, centerObject, planePosition + forwardOffset + rightOffset);
        GameObject miniPlane2 = CreateMiniPlane(original, centerObject, planePosition + forwardOffset - rightOffset);
        GameObject miniPlane3 = CreateMiniPlane(original, centerObject, planePosition - forwardOffset + rightOffset);
        GameObject miniPlane4 = CreateMiniPlane(original, centerObject, planePosition - forwardOffset - rightOffset);

        ObjectRepository.planes.Remove(original);
        Destroy(original);

        ObjectRepository.miniPlanes.Add(miniPlane1);
        ObjectRepository.miniPlanes.Add(miniPlane2);
        ObjectRepository.miniPlanes.Add(miniPlane3);
        ObjectRepository.miniPlanes.Add(miniPlane4);
    }

    private GameObject CreateCenterObject(GameObject original)
    {
        GameObject centerObject = new GameObject("planeSide");
        centerObject.transform.position = original.transform.position;
        centerObject.transform.parent = original.transform.parent;
        return centerObject;
    }

    private GameObject CreateMiniPlane(GameObject original, GameObject parent, Vector3 position)
    {
        GameObject miniPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        miniPlane.name = "miniPlane";

        Color originalColor = original.GetComponent<Renderer>().material.color;
        miniPlane.GetComponent<Renderer>().material.color = originalColor;

        miniPlane.transform.parent = parent.transform;
        miniPlane.transform.position = position;
        miniPlane.transform.rotation = original.transform.rotation;
        miniPlane.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

        return miniPlane;
    }
}
