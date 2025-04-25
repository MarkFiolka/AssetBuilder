using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Interaction
{
    public class BlockPlacements : MonoBehaviour
    {
        private const float threshold = 0.222f;
        private const float positionCheckTolerance = 0.001f;

        private Vector3? lastStartPosition = null;
        private Vector3? placeDirection = null;
        private float placedDistance = 0f;
        private float stepSize = 1f;

        private Plane? imaginaryPlane = null;
        private Vector3 gridOrigin;
        private Vector3 gridNormal;
        private List<GameObject> gridPreviews = new List<GameObject>();
        private bool gridInitialized = false;

        void Update()
        {
            bool altHeld = Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);

            if (altHeld && !gridInitialized && !Input.GetMouseButton(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 faceCenter = hit.collider.transform.position;
                    imaginaryPlane = new Plane(hit.normal, faceCenter);
                    gridOrigin = faceCenter;
                    gridNormal = hit.normal;
                    GenerateGrid(gridOrigin, gridNormal);
                    gridInitialized = true;
                }
            }

            if (Input.GetMouseButtonDown(1) && Settings.Instance.placeBlocks)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (altHeld && imaginaryPlane.HasValue && imaginaryPlane.Value.Raycast(ray, out float altEnter))
                {
                    Vector3 point = ray.GetPoint(altEnter);
                    Vector3 targetPos = RoundToGrid(point);
                    ReplacePreviewWithBlock(targetPos);
                    lastStartPosition = targetPos;
                    placeDirection = gridNormal;
                    placedDistance = 0f;
                }
                else if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.CompareTag("cube"))
                    {
                        Vector3 placePos = hit.collider.transform.position + hit.normal;
                        SpawnBlock(placePos);
                        lastStartPosition = placePos;
                        placeDirection = hit.normal;
                        placedDistance = 0f;
                    }
                }
            }

            if (Input.GetMouseButton(1) && lastStartPosition.HasValue && placeDirection.HasValue)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane dragPlane = imaginaryPlane ?? new Plane(placeDirection.Value, lastStartPosition.Value);

                if (dragPlane.Raycast(ray, out float enter))
                {
                    Vector3 point = ray.GetPoint(enter);
                    float projected = Vector3.Dot((point - lastStartPosition.Value), placeDirection.Value);

                    while (projected >= placedDistance + stepSize)
                    {
                        Vector3 spawnPos = lastStartPosition.Value + placeDirection.Value * (placedDistance + stepSize);
                        if (!IsBlockAtPosition(spawnPos))
                        {
                            ReplacePreviewWithBlock(spawnPos);
                        }
                        placedDistance += stepSize;
                    }
                }
            }

            if (!altHeld && gridInitialized)
            {
                imaginaryPlane = null;
                ClearGrid();
                gridInitialized = false;
            }
        }

        private void GenerateGrid(Vector3 origin, Vector3 normal)
        {
            ClearGrid();

            Vector3 right = Vector3.Cross(normal, Vector3.up).magnitude > 0.1f
                ? Vector3.Cross(normal, Vector3.up).normalized
                : Vector3.Cross(normal, Vector3.right).normalized;

            Vector3 forward = Vector3.Cross(right, normal).normalized;

            float spacing = 1f;
            int gridCount = 2; // für 5x5 Grid (2 Blöcke links/rechts + Mitte)

            Vector3 gridOffset = (-right + -forward) * 0.5f;
            Vector3 gridCenterOffset = right * -gridCount * spacing + forward * -gridCount * spacing;
            Vector3 baseOrigin = origin + gridOffset + gridCenterOffset;

            for (int x = 0; x <= gridCount * 2; x++)
            {
                for (int z = 0; z <= gridCount * 2; z++)
                {
                    Vector3 cubePos = baseOrigin + right * x * spacing + forward * z * spacing;
                    SpawnGhostCube(cubePos);
                }
            }
        }

        private void SpawnGhostCube(Vector3 position)
        {
            GameObject ghost = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ghost.transform.position = position;
            ghost.transform.localScale = Vector3.one;
            ghost.tag = "GridPreviewBlock";
            ghost.name = "GridPreviewBlock";

            // Material transparent
            var renderer = ghost.GetComponent<MeshRenderer>();
            var material = new Material(Shader.Find("Unlit/Color"));
            material.color = new Color(1f, 1f, 1f, 0.05f); // Sehr durchsichtig
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
            renderer.material = material;

            Destroy(ghost.GetComponent<Collider>());

            // Kanten mit LineRenderer
            LineRenderer edge = ghost.AddComponent<LineRenderer>();
            edge.positionCount = 16;
            edge.loop = false;
            edge.widthMultiplier = 0.015f;
            edge.material = new Material(Shader.Find("Unlit/Color"));
            edge.material.color = new Color(1f, 1f, 1f, 0.4f);

            Vector3[] corners = new Vector3[]
            {
                new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, -0.5f, 0.5f),  new Vector3(-0.5f, -0.5f, 0.5f),
                new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-0.5f, 0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, -0.5f), new Vector3(0.5f, -0.5f, -0.5f),
                new Vector3(0.5f, 0.5f, -0.5f), new Vector3(0.5f, 0.5f, 0.5f),
                new Vector3(0.5f, -0.5f, 0.5f), new Vector3(-0.5f, -0.5f, 0.5f),
                new Vector3(-0.5f, 0.5f, 0.5f), new Vector3(-0.5f, -0.5f, 0.5f),
                new Vector3(-0.5f, 0.5f, -0.5f), new Vector3(-0.5f, 0.5f, 0.5f),
            };

            for (int i = 0; i < corners.Length; i++)
                corners[i] += ghost.transform.position;

            edge.SetPositions(corners);

            gridPreviews.Add(ghost);
        }

        private void ReplacePreviewWithBlock(Vector3 position)
        {
            GameObject preview = gridPreviews.Find(g => Vector3.Distance(g.transform.position, position) < positionCheckTolerance);
            if (preview != null)
            {
                gridPreviews.Remove(preview);
                Destroy(preview);
            }

            SpawnBlock(position);
        }

        private void ClearGrid()
        {
            foreach (var g in gridPreviews)
            {
                if (g) Destroy(g);
            }
            gridPreviews.Clear();
        }

        private Vector3 RoundToGrid(Vector3 position)
        {
            return new Vector3(
                Mathf.Round(position.x),
                Mathf.Round(position.y),
                Mathf.Round(position.z)
            );
        }

        private void SpawnBlock(Vector3 spawnPos)
        {
            if (IsBlockAtPosition(spawnPos)) return;

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = spawnPos;
            cube.tag = "cube";
            cube.name = "cube";
            cube.layer = 0;
            cube.AddComponent<CubeRegistration>();

            var mr = cube.GetComponent<MeshRenderer>();
            mr.material.color = Settings.Instance.color;

            var container = GameObject.Find("SzeneContainer");
            if (container != null)
                cube.transform.SetParent(container.transform, true);

            Logger.Log($"Spawned block at {spawnPos}");
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
