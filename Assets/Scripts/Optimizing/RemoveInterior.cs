using UnityEngine;
using System.Collections.Generic;
using Utility;

namespace Optimizing
{
    public class RemoveInterior : MonoBehaviour
    {
        public void RunOptimizationRemoveInteriorPlanesWithinMesh()
        {
            RemoveInteriorPlanesWithinMesh();
        }

        void RemoveInteriorPlanesWithinMesh()
        {
            List<GameObject> miniPlanes = new List<GameObject>(ObjectRepository.miniPlanes);
            List<GameObject> toRemove = new List<GameObject>();

            for (int i = 0; i < miniPlanes.Count; i++)
            {
                GameObject obj1 = miniPlanes[i];
                if (obj1 == null)
                    continue;

                for (int j = i + 1; j < miniPlanes.Count; j++)
                {
                    GameObject obj2 = miniPlanes[j];
                    if (obj2 == null)
                        continue;

                    if (obj1.transform.position == obj2.transform.position)
                    {
                        if (obj1.transform.rotation != obj2.transform.rotation)
                        {
                            if (!toRemove.Contains(obj1))
                                toRemove.Add(obj1);
                            if (!toRemove.Contains(obj2))
                                toRemove.Add(obj2);
                        }
                        else
                        {
                            if (!toRemove.Contains(obj2))
                                toRemove.Add(obj2);
                        }
                    }
                }
            }

            foreach (var obj in toRemove)
            {
                if (obj != null)
                {
                    ObjectRepository.miniPlanes.Remove(obj);
                    DestroyImmediate(obj);
                }
            }

            Logger.Log($"Removed {toRemove.Count} objects from the scene.");
        }
    }

}