namespace UndoRedo
{
    using UnityEngine;

    [RequireComponent(typeof(Renderer))]
    public class TrackableObject : MonoBehaviour
    {
        private Vector3 _lastPos;
        private Color _lastCol;
        private Renderer _rend;

        void Awake()
        {
            _rend = GetComponent<Renderer>();
            _lastPos = transform.position;
            _lastCol = _rend.material.color;
        }

        void LateUpdate()
        {
            var p = transform.position;
            if (p != _lastPos)
            {
                ChangeTracker.NotifyPositionChanged(gameObject, _lastPos, p);
                _lastPos = p;
            }

            var c = _rend.material.color;
            if (c != _lastCol)
            {
                ChangeTracker.NotifyColorChanged(gameObject, _lastCol, c);
                _lastCol = c;
            }
        }

        void OnDestroy()
        {
            ChangeTracker.NotifyObjectDeleted(gameObject);
        }
    }
}