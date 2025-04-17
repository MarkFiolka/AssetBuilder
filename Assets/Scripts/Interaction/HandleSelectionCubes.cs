using System.Collections.Generic;
using UnityEngine;
using Utility;

public class HandleSelectionCubes : MonoBehaviour
{
    private Vector3 _dragStart;
    private bool _dragging;
    private const float Threshold = 5f;

    void Update()
    {
        if (!Settings.Instance.selectCubes) return;

        if (Input.GetMouseButtonDown(0) && !Settings.Instance.isMoving)
        {
            _dragStart = Input.mousePosition;
            _dragging = true;
        }

        if (Input.GetMouseButtonUp(0) && _dragging && !Settings.Instance.isMoving)
        {
            float d = Vector3.Distance(_dragStart, Input.mousePosition);
            if (d > Threshold) BoxSelect(); else SingleSelect();
            _dragging = false;
        }
    }

    void OnGUI()
    {
        if (!_dragging || !Settings.Instance.selectCubes || Settings.Instance.isMoving) return;
        var rect = Utils.GetScreenRect(_dragStart, Input.mousePosition);
        Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
        Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
    }

    private void SingleSelect()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit) && ObjectRepository.cubes.Contains(hit.collider.gameObject))
            Toggle(hit.collider.gameObject);
        else
            ClearSelection();
    }

    private void BoxSelect()
    {
        var cam = Camera.main;
        var bounds = Utils.GetViewportBounds(cam, _dragStart, Input.mousePosition);
        ClearSelection();
        foreach (var cube in ObjectRepository.cubes)
        {
            var vp = cam.WorldToViewportPoint(cube.transform.position);
            if (bounds.Contains(vp)) Select(cube);
        }
    }

    private void Toggle(GameObject c)
    {
        var r = c.GetComponent<Renderer>();
        if (ObjectRepository.selectedCubes.Contains(c))
        {
            r.material.color = ObjectRepository.originalCubeColors[c];
            ObjectRepository.originalCubeColors.Remove(c);
            ObjectRepository.selectedCubes.Remove(c);
        }
        else
        {
            ObjectRepository.originalCubeColors[c] = r.material.color;
            r.material.color = Settings.Instance.selectColor;
            ObjectRepository.selectedCubes.Add(c);
        }
    }

    private void Select(GameObject c)
    {
        if (ObjectRepository.selectedCubes.Contains(c)) return;
        var r = c.GetComponent<Renderer>();
        ObjectRepository.originalCubeColors[c] = r.material.color;
        r.material.color = Settings.Instance.selectColor;
        ObjectRepository.selectedCubes.Add(c);
    }

    private void ClearSelection()
    {
        foreach (var c in new List<GameObject>(ObjectRepository.selectedCubes))
        {
            var r = c.GetComponent<Renderer>();
            if (ObjectRepository.originalCubeColors.TryGetValue(c, out var o))
                r.material.color = o;
            else
                Logger.LogWarning($"No original color for {c.name}");
        }
        ObjectRepository.selectedCubes.Clear();
        ObjectRepository.originalCubeColors.Clear();
    }
}