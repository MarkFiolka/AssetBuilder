using DefaultNamespace;
using UnityEngine;

public class PositionGetter : MonoBehaviour
{
    public float maxDistance = 100f;
    private BlockPlacements bp;
    private BlockBreaker bb;
    private BlockColorer bc;
    private SelectCubes sc;
    
    void Start()
    {
        GetBlockPlacements();
    }

    private void GetBlockPlacements()
    {
        bp = GameObject.FindObjectOfType<BlockPlacements>();
        bb = GameObject.FindObjectOfType<BlockBreaker>();
        bc = GameObject.FindObjectOfType<BlockColorer>();
        sc = GameObject.FindObjectOfType<SelectCubes>();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (Settings.Instance.placeBlocks)
                {
                    if (bp != null)
                    {
                        bp.CalculatePlacement(hit);
                    }
                }

                if (Settings.Instance.breakBlocks)
                {
                    if (bb != null)
                    {
                        bb.CalculateBreaking(hit);
                    }
                }
                
                if (Settings.Instance.colorBlocks)
                {
                    if (bc != null)
                    {
                        bc.CalculateColoring(hit);
                    }
                }

                if (Settings.Instance.selectCubes)
                {
                    if (sc != null)
                    {
                        sc.HandleHitCube(hit);
                    }
                }
            }
        }
    }
}