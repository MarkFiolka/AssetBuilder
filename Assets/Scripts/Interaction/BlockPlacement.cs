using UnityEngine;
using Utility;

namespace Interaction
{
    public class BlockPlacements : MonoBehaviour
    {
        private const float threshold = 0.222f;
        private const float positionCheckTolerance = 0.001f;


        public void CalculatePlacement(RaycastHit hit)
        {
            if (hit.collider.gameObject.name == "cube")
            {
                if (Settings.Instance.halfPlacement == false)
                {
                    CalcNormalPosPlacement(hit);
                }
                else
                {
                    CalcStrgPosPlacement(hit);
                }
            }
            else
            {
                Logger.Log("Can't be placed");
            }
        }

        public void CalcNormalPosPlacement(RaycastHit hit)
        {
            Vector3 hitOnNormalPos = hit.point - hit.collider.gameObject.transform.position;
            CalculatePositionPlacement(hitOnNormalPos, hit.normal, hit, 1f);
        }

        public void CalcStrgPosPlacement(RaycastHit hit)
        {
            Vector3 hitOnNormalPos = hit.point - hit.collider.gameObject.transform.position;
            CalculatePositionPlacement(hitOnNormalPos, hit.normal, hit, 0.5f);
        }

        private void SpawnBlock(Vector3 spawnPos, RaycastHit hit)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = spawnPos;
            cube.name = "cube";
            MeshRenderer mr = cube.GetComponent<MeshRenderer>();
            mr.material.color = Settings.Instance.color;
            GameObject myGameObject = GameObject.Find("SzeneContainer");
            cube.transform.parent = myGameObject.transform;
            ObjectRepository.cubes.Add(cube);
        }

        private void CalculatePositionPlacement(Vector3 hitOnNormalPos, Vector3 hitNormal, RaycastHit hit,
            float multiplier)
        {
            Vector2 relativeHit = Vector2.zero;
            Vector3 basePosition = hit.transform.position + multiplier * hit.normal;
            if (Mathf.Abs(hitNormal.x) > 0.5f)
            {
                relativeHit = new Vector2(hitOnNormalPos.y, hitOnNormalPos.z);
            }
            else if (Mathf.Abs(hitNormal.y) > 0.5f)
            {
                relativeHit = new Vector2(hitOnNormalPos.x, hitOnNormalPos.z);
            }
            else if (Mathf.Abs(hitNormal.z) > 0.5f)
            {
                relativeHit = new Vector2(hitOnNormalPos.x, hitOnNormalPos.y);
            }

            int offsetA = GetGridOffset(relativeHit.x);
            int offsetB = GetGridOffset(relativeHit.y);
            Vector3 finalOffset = Vector3.zero;
            if (Mathf.Abs(hitNormal.x) > 0.5f)
            {
                finalOffset = new Vector3(0, offsetA, offsetB);
            }
            else if (Mathf.Abs(hitNormal.y) > 0.5f)
            {
                finalOffset = new Vector3(offsetA, 0, offsetB);
            }
            else if (Mathf.Abs(hitNormal.z) > 0.5f)
            {
                finalOffset = new Vector3(offsetA, offsetB, 0);
            }

            Vector3 spawnPos = basePosition + multiplier * finalOffset;
            if (IsBlockAtPosition(spawnPos))
            {
                Logger.Log("A block already exists at " + spawnPos + ". Placement aborted.");
                return;
            }

            SpawnBlock(spawnPos, hit);
            Logger.Log($"Spawned block at {spawnPos} using hit normal {hitNormal} with multiplier {multiplier}");
        }

        private int GetGridOffset(float coordinate)
        {
            if (coordinate > threshold) return 1;
            else if (coordinate < -threshold) return -1;
            else return 0;
        }

        private bool IsBlockAtPosition(Vector3 position)
        {
            foreach (GameObject cube in ObjectRepository.cubes)
            {
                if (cube != null && Vector3.Distance(cube.transform.position, position) < positionCheckTolerance)
                {
                    return true;
                }
            }

            return false;
        }
    }
}