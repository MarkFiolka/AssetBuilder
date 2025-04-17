using UnityEngine;

namespace Utility
{
    public static class Utils
    {
        private static Texture2D _whiteTexture;
        private static Texture2D WhiteTexture
        {
            get
            {
                if (_whiteTexture == null)
                {
                    _whiteTexture = new Texture2D(1, 1);
                    _whiteTexture.SetPixel(0, 0, Color.white);
                    _whiteTexture.Apply();
                }
                return _whiteTexture;
            }
        }

        public static Rect GetScreenRect(Vector3 p1, Vector3 p2)
        {
            p1.y = Screen.height - p1.y;
            p2.y = Screen.height - p2.y;
            float x = Mathf.Min(p1.x, p2.x);
            float y = Mathf.Min(p1.y, p2.y);
            float w = Mathf.Abs(p1.x - p2.x);
            float h = Mathf.Abs(p1.y - p2.y);
            return new Rect(x, y, w, h);
        }

        public static Bounds GetViewportBounds(Camera cam, Vector3 p1, Vector3 p2)
        {
            Vector3 v1 = cam.ScreenToViewportPoint(p1);
            Vector3 v2 = cam.ScreenToViewportPoint(p2);
            Vector3 min = Vector3.Min(v1, v2);
            Vector3 max = Vector3.Max(v1, v2);
            min.z = cam.nearClipPlane;
            max.z = cam.farClipPlane;
            Bounds b = new Bounds();
            b.SetMinMax(min, max);
            return b;
        }

        public static void DrawScreenRect(Rect r, Color c)
        {
            GUI.color = c;
            GUI.DrawTexture(r, WhiteTexture);
            GUI.color = Color.white;
        }

        public static void DrawScreenRectBorder(Rect r, float t, Color c)
        {
            DrawScreenRect(new Rect(r.xMin, r.yMin, r.width, t), c);
            DrawScreenRect(new Rect(r.xMin, r.yMin, t, r.height), c);
            DrawScreenRect(new Rect(r.xMax - t, r.yMin, t, r.height), c);
            DrawScreenRect(new Rect(r.xMin, r.yMax - t, r.width, t), c);
        }
    }
}