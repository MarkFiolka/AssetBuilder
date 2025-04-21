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
            if (Settings.Instance.selectCubes) return;

            if (hit.collider.gameObject.CompareTag("cube"))
            {
                if (!Settings.Instance.halfPlacement)
                    CalcNormalPosPlacement(hit);
                else
                    CalcStrgPosPlacement(hit);
            }
            else
            {
                Logger.Log("Can't be placed");
            }
        }

        public void CalcNormalPosPlacement(RaycastHit hit)
        {
            Vector3 hitOnNormalPos = hit.point - hit.collider.transform.position;
            CalculatePositionPlacement(hitOnNormalPos, hit.normal, hit, 1f);
        }

        public void CalcStrgPosPlacement(RaycastHit hit)
        {
            Vector3 hitOnNormalPos = hit.point - hit.collider.transform.position;
            CalculatePositionPlacement(hitOnNormalPos, hit.normal, hit, 0.5f);
        }

        private void SpawnBlock(Vector3 spawnPos)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = spawnPos;
            cube.tag = "cube";
            cube.name = "cube";
            cube.AddComponent<CubeRegistration>();

            var mr = cube.GetComponent<MeshRenderer>();
            mr.material.color = Settings.Instance.color;

            var container = GameObject.Find("SzeneContainer");
            if (container != null)
                cube.transform.SetParent(container.transform, true);
            
            Logger.Log($"Spawned block at {spawnPos}");
        }

        private void CalculatePositionPlacement(Vector3 hitOnNormalPos, Vector3 hitNormal, RaycastHit hit, float multiplier)
        {
            Vector2 relativeHit = Vector2.zero;
            Vector3 basePos = hit.transform.position + multiplier * hit.normal;

            if (Mathf.Abs(hitNormal.x) > 0.5f)
                relativeHit = new Vector2(hitOnNormalPos.y, hitOnNormalPos.z);
            else if (Mathf.Abs(hitNormal.y) > 0.5f)
                relativeHit = new Vector2(hitOnNormalPos.x, hitOnNormalPos.z);
            else if (Mathf.Abs(hitNormal.z) > 0.5f)
                relativeHit = new Vector2(hitOnNormalPos.x, hitOnNormalPos.y);

            int offsetA = GetGridOffset(relativeHit.x);
            int offsetB = GetGridOffset(relativeHit.y);
            Vector3 finalOffset = Vector3.zero;

            if (Mathf.Abs(hitNormal.x) > 0.5f)
                finalOffset = new Vector3(0, offsetA, offsetB);
            else if (Mathf.Abs(hitNormal.y) > 0.5f)
                finalOffset = new Vector3(offsetA, 0, offsetB);
            else if (Mathf.Abs(hitNormal.z) > 0.5f)
                finalOffset = new Vector3(offsetA, offsetB, 0);

            Vector3 spawnPos = basePos + multiplier * finalOffset;
            if (IsBlockAtPosition(spawnPos))
            {
                Logger.Log("A block already exists at " + spawnPos + ". Placement aborted.");
                return;
            }

            SpawnBlock(spawnPos);
        }

        private int GetGridOffset(float coordinate)
        {
            if (coordinate > threshold) return 1;
            if (coordinate < -threshold) return -1;
            return 0;
        }

        private bool IsBlockAtPosition(Vector3 position)
        {
            foreach (var cube in ObjectRepository.cubes)
            {
                if (cube && Vector3.Distance(cube.transform.position, position) < positionCheckTolerance)
                    return true;
            }
            return false;
        }
    }
}