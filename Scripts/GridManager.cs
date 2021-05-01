using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2 rangeMinMax;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private List<GameObject> tilesList = new List<GameObject>();
    [SerializeField] private GameObject[] tilesArray;


    private void Start()
    {
        tilesArray = GameObject.FindGameObjectsWithTag("Tile");
        TilesLoop();
    }
    private void TilesLoop()
    {
        for (int i = 0; i < tilesArray.Length; i++)
        {
            tilesList.Add(tilesArray[i]);
        }
    }

    private void InfoWithTheList()
    {
        List<TileScript> tilesScripts = new List<TileScript>();
        foreach (GameObject tile in tilesList)
        {
            if (tile.GetComponent<TileScript>() != null) tilesScripts.Add(tile.GetComponent<TileScript>());
        }
        GameManager.managerGame.systemPathfinding.tiles = tilesScripts;
    }
    public void CreateAGrid()
    {
        tilesList.Clear();
        GameObject tileManager = new GameObject();
        tileManager.name = "Tile Manager";
        for (int x = 0; x < rangeMinMax.x; x++)
        {
            for (int y = 0; y < rangeMinMax.y; y++)
            {
                GameObject go = Instantiate(tilePrefab, new Vector3(x, 0f, y), Quaternion.identity);
                go.transform.parent = tileManager.transform;
                tilesList.Add(go);
            }
        }

        InfoWithTheList();
    }

    public void DestroyGrid()
    {
        for (int i = 0; i < tilesList.Count; i++)
        {
           DestroyImmediate (tilesList[i]);
        }
       
    }

    public void PutTheObstacles()
    {
        for (int i = 0; i <    tilesList.Count; i++)
        {
            if (tilesList[i].GetComponent<TileScript>().obstacleValueBool)
            {
                tilesList[i].GetComponent<TileScript>().IncrementObstaclueValue();
            }
        }
    }
}
