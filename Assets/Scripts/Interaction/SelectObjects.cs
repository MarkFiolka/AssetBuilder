using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

public class SelectObjects : MonoBehaviour
{
    private Vector3 _dragStart;
    private bool _dragging;
    private const float Threshold = 5f;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!Settings.Instance.selectBlocks || Settings.Instance.isMoving)
            return;

        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
        {
            _dragStart = Input.mousePosition;
            _dragging = true;
        }

        if (Input.GetMouseButtonUp(0) && _dragging)
        {
            float dist = Vector3.Distance(_dragStart, Input.mousePosition);
            if (dist > Threshold) BoxSelect();
            else SingleSelect();
            _dragging = false;
        }
    }

    private void OnGUI()
    {
        if (!_dragging || !Settings.Instance.selectBlocks || Settings.Instance.isMoving || IsPointerOverUI())
            return;

        Rect rect = Utils.GetScreenRect(_dragStart, Input.mousePosition);
        Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
        Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
    }

    private void SingleSelect()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && ObjectRepository.cubes.Contains(hit.collider.gameObject))
            Toggle(hit.collider.gameObject);
        else
            ClearSelection();
    }

    private void BoxSelect()
    {
        var bounds = Utils.GetViewportBounds(_camera, _dragStart, Input.mousePosition);
        ClearSelection();
        foreach (var cube in ObjectRepository.cubes)
        {
            Vector3 vp = _camera.WorldToViewportPoint(cube.transform.position);
            if (bounds.Contains(vp))
                Select(cube);
        }
    }

    private void Toggle(GameObject obj)
    {
        var rend = obj.GetComponent<Renderer>();
        if (ObjectRepository.selectedCubes.Contains(obj))
        {
            rend.material.color = ObjectRepository.originalCubeColors[obj];
            ObjectRepository.originalCubeColors.Remove(obj);
            ObjectRepository.selectedCubes.Remove(obj);
        }
        else
        {
            ObjectRepository.originalCubeColors[obj] = rend.material.color;
            rend.material.color = Settings.Instance.selectColor;
            ObjectRepository.selectedCubes.Add(obj);
        }
    }

    private void Select(GameObject obj)
    {
        if (ObjectRepository.selectedCubes.Contains(obj)) return;
        var rend = obj.GetComponent<Renderer>();
        ObjectRepository.originalCubeColors[obj] = rend.material.color;
        rend.material.color = Settings.Instance.selectColor;
        ObjectRepository.selectedCubes.Add(obj);
    }

    private void ClearSelection()
    {
        foreach (var obj in new List<GameObject>(ObjectRepository.selectedCubes))
        {
            if (ObjectRepository.originalCubeColors.TryGetValue(obj, out var orig))
                obj.GetComponent<Renderer>().material.color = orig;
        }
        ObjectRepository.selectedCubes.Clear();
        ObjectRepository.originalCubeColors.Clear();
    }

    private bool IsPointerOverUI()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return true;

        if (GUIUtility.hotControl != 0)
            return true;

        return false;
    }
}
