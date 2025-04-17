using UnityEngine;
using Utility;

namespace Interaction
{
    public class PositionGetter : MonoBehaviour
    {
        public float maxDistance = 100f;
        private BlockPlacements bp;
        private BlockBreaker bb;
        private BlockColorer bc;
        private HandleSelectionCubes sc;

        void Start()
        {
            bp = FindObjectOfType<BlockPlacements>();
            bb = FindObjectOfType<BlockBreaker>();
            bc = FindObjectOfType<BlockColorer>();
            sc = FindObjectOfType<HandleSelectionCubes>();
        }

        void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (!Settings.Instance.placeBlocks && !Settings.Instance.breakBlocks && !Settings.Instance.paintBlocks)
                return; // selection handled in HandleSelectionCubes

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, maxDistance)) return;

            if (Settings.Instance.placeBlocks && bp != null)
                bp.CalculatePlacement(hit);
            if (Settings.Instance.breakBlocks && bb != null)
                bb.CalculateBreaking(hit);
            if (Settings.Instance.paintBlocks && bc != null)
                bc.CalculateColoring(hit);
        }
    }
}