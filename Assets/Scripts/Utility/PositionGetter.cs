using UnityEngine;
using Utility;

namespace Interaction
{
    public class PositionGetter : MonoBehaviour
    {
        public float maxDistance = 100f;
        private BlockPlacements bp;
        private BlockBreaker bb;
        private BlockPainter bc;
        private SelectObjects sc;

        void Start()
        {
            bp = FindObjectOfType<BlockPlacements>();
            bb = FindObjectOfType<BlockBreaker>();
            bc = FindObjectOfType<BlockPainter>();
            sc = FindObjectOfType<SelectObjects>();
        }

        void Update()
        {
            bool isPainting = Settings.Instance.paintBlocks && Input.GetMouseButton(0);
            bool isPlaceOrBreak = (Settings.Instance.placeBlocks || Settings.Instance.breakBlocks) && Input.GetMouseButtonDown(0);

            if (!isPainting && !isPlaceOrBreak)
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, maxDistance))
                return;

            if (Settings.Instance.placeBlocks && Input.GetMouseButtonDown(0) && bp != null)
                bp.CalculatePlacement(hit);

            if (Settings.Instance.breakBlocks && Input.GetMouseButtonDown(0) && bb != null)
                bb.CalculateBreaking(hit);

            if (Settings.Instance.paintBlocks && bc != null && isPainting)
                bc.CalculateColoring(hit);
        }
    }
}