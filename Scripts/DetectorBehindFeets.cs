using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorBehindFeets : MonoBehaviour
{
    [SerializeField] private float aimBehindOurFeets;
    public TileScript tileBehindOurFeets;
    public void Update()
    {
        RayOurFeets();
    }

    private void RayOurFeets()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                tileBehindOurFeets = hit.collider.GetComponent<TileScript>();
            }
        }
    }
}
