using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectAllCovers : MonoBehaviour
{
    [SerializeField] private List<TileScript> coverTiles = new List<TileScript>();
    private void Start()
    {
        Invoke("UpdateInfo", 3f);
       
    }

    private void UpdateInfo()
    {
        TileScript[] tiles = FindObjectsOfType<TileScript>();

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].coverValueBool)
            {
                coverTiles.Add(tiles[i]);
            }
        }
    }
}
