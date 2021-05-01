using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Vector3 offsetTile;
    public LayerMask maskLayerNotTile;


    private void Awake()
    {
        GameManager.managerGame.managerTile = this;
    }
}
