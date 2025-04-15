using UnityEditor;
using UnityEngine;

public class HoverPartialGrid : MonoBehaviour
{
    public string CurrentSubTileName { get; private set; } = "None";
    [SerializeField] float faceTileSize = 1f;
    [SerializeField] float faceTileOffset = 0.001f;
    [SerializeField] float ghostScale = 1f;
    [SerializeField] float threshold = 0.222f;
    [SerializeField] float positionCheckTolerance = 0.001f;
    [SerializeField] Color faceTileColor = new Color(1f, 1f, 0f, 0.35f);
    [SerializeField] Color ghostColor = new Color(1f, 1f, 1f, 0.3f);
    [SerializeField] Color ghostBlockedColor = new Color(1f, 0f, 0f, 0.3f);

    Camera cam;
    GameObject faceTile;
    GameObject ghostCube;
    MeshRenderer faceTileR;
    MeshRenderer ghostCubeR;

    void Awake()
    {
        cam = Camera.main;
    }

    void Start()
    {
        faceTile = CreatePreview(PrimitiveType.Quad, faceTileColor, out faceTileR);
        ghostCube = CreatePreview(PrimitiveType.Cube, ghostColor, out ghostCubeR);
    }

    void Update()
    {
        if (!cam) return;

        faceTile.SetActive(false);
        ghostCube.SetActive(false);
        CurrentSubTileName = "None";

        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit h))
            return;
        if (h.collider.name != "cube")
            return;

        float m = (Settings.Instance.halfPlacement) ? 0.5f : 1f;

        (int a, int b) = GetOffsets(h);
        CurrentSubTileName = GetSubTileName(a, b);

        if (Settings.Instance.placementGridPreview)
            ShowFaceSubTile(h, a, b);

        if (!Settings.Instance.placementPreview)
            return;

        Vector3 sp = GetSpawnPos(h, m, a, b);
        ghostCubeR.material.color = IsBlocked(sp) ? ghostBlockedColor : ghostColor;
        ghostCube.transform.position = sp;
        ghostCube.SetActive(true);
    }

    GameObject CreatePreview(PrimitiveType t, Color c, out MeshRenderer r)
    {
        GameObject o = GameObject.CreatePrimitive(t);
        Destroy(o.GetComponent<Collider>());
        o.layer = LayerMask.NameToLayer("Ignore Raycast");
        r = o.GetComponent<MeshRenderer>();
        Material m = new Material(Shader.Find("Unlit/Color")) { color = c };
        r.sharedMaterial = m;
        o.SetActive(false);
        return o;
    }

    bool IsBlocked(Vector3 p)
    {
        foreach (var c in ObjectRepository.cubes)
            if (c && Vector3.Distance(c.transform.position, p) < positionCheckTolerance)
                return true;
        return false;
    }

    (int, int) GetOffsets(RaycastHit h)
    {
        Vector3 n = h.normal;
        Vector3 lp = h.collider.transform.InverseTransformPoint(h.point);
        Vector2 r = Mathf.Abs(n.x) > 0.5f ? new Vector2(lp.y, lp.z)
                  : Mathf.Abs(n.y) > 0.5f ? new Vector2(lp.x, lp.z)
                  : new Vector2(lp.x, lp.y);
        return (GridOffset(r.x), GridOffset(r.y));
    }

    int GridOffset(float v)
    {
        return v > threshold ? 1 : v < -threshold ? -1 : 0;
    }

    string GetSubTileName(int a, int b)
    {
        if (a == 1 && b == -1) return "top-left";
        if (a == 1 && b == 0) return "top-center";
        if (a == 1 && b == 1) return "top-right";
        if (a == 0 && b == -1) return "middle-left";
        if (a == 0 && b == 0) return "center";
        if (a == 0 && b == 1) return "middle-right";
        if (a == -1 && b == -1) return "bottom-left";
        if (a == -1 && b == 0) return "bottom-center";
        if (a == -1 && b == 1) return "bottom-right";
        return "Unknown";
    }

    Vector3 GetSpawnPos(RaycastHit h, float m, int a, int b)
    {
        Vector3 p = h.collider.transform.position + h.normal * m;
        Vector3 f = Vector3.zero;
        if (Mathf.Abs(h.normal.x) > 0.5f)
            f = new Vector3(0, a, b);
        else if (Mathf.Abs(h.normal.y) > 0.5f)
            f = new Vector3(a, 0, b);
        else
            f = new Vector3(a, b, 0);
        return p + m * f;
    }

    void ShowFaceSubTile(RaycastHit h, int a, int b)
    {
        Bounds cubeBounds = h.collider.bounds;
        Vector3 cubeSize = cubeBounds.size;
        Vector3 localFaceCenter = Vector3.zero;

        if (Mathf.Abs(h.normal.x) > 0.5f)
            localFaceCenter = new Vector3(Mathf.Sign(h.normal.x) * cubeSize.x * 0.5f, 0, 0);
        else if (Mathf.Abs(h.normal.y) > 0.5f)
            localFaceCenter = new Vector3(0, Mathf.Sign(h.normal.y) * cubeSize.y * 0.5f, 0);
        else
            localFaceCenter = new Vector3(0, 0, Mathf.Sign(h.normal.z) * cubeSize.z * 0.5f);

        Vector3 localHit = h.collider.transform.InverseTransformPoint(h.point);

        Vector2 localOffset = Vector2.zero;
        if (Mathf.Abs(h.normal.x) > 0.5f)
            localOffset = new Vector2(localHit.y - localFaceCenter.y, localHit.z - localFaceCenter.z);
        else if (Mathf.Abs(h.normal.y) > 0.5f)
            localOffset = new Vector2(localHit.x - localFaceCenter.x, localHit.z - localFaceCenter.z);
        else
            localOffset = new Vector2(localHit.x - localFaceCenter.x, localHit.y - localFaceCenter.y);

        float cellWidthX = cubeSize.x / 3f;
        float cellWidthY = cubeSize.y / 3f;
        float halfCellWidthX = cellWidthX / 2f;
        float halfCellWidthY = cellWidthY / 2f;

        float snappedX = Mathf.Round(localOffset.x / cellWidthX) * cellWidthX;
        float snappedY = Mathf.Round(localOffset.y / cellWidthY) * cellWidthY;

        Vector3 finalLocal = localFaceCenter;
        if (Mathf.Abs(h.normal.x) > 0.5f)
        {
            finalLocal.y += snappedX;
            finalLocal.z += snappedY;
        }
        else if (Mathf.Abs(h.normal.y) > 0.5f)
        {
            finalLocal.x += snappedX;
            finalLocal.z += snappedY;
        }
        else
        {
            finalLocal.x += snappedX;
            finalLocal.y += snappedY;
        }

        Vector3 finalWorld = h.collider.transform.TransformPoint(finalLocal);

        finalWorld += h.normal * faceTileOffset;

        faceTile.transform.localScale = new Vector3(cellWidthX, cellWidthY, 1f);
        faceTile.transform.position = finalWorld;
        faceTile.transform.rotation = Quaternion.LookRotation(-h.normal, Vector3.up);
        faceTile.SetActive(true);
    }
}
