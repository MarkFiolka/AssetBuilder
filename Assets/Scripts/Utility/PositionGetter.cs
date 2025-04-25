using UnityEngine;
using Utility;

namespace Interaction
{
    public class PositionGetter : MonoBehaviour
    {
        public float maxDistance = 100f;
        private BlockPlacements bp;
        private BlockPainter bc;

        void Start()
        {
            bp = FindObjectOfType<BlockPlacements>();
            bc = FindObjectOfType<BlockPainter>();
        }

        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, maxDistance))
                return;

            if (Settings.Instance.paintBlocks && bc != null)
                bc.CalculateColoring(hit);
        }
    }
}