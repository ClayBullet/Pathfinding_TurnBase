using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingSystem : MonoBehaviour
{

    public List<TileScript> tilesUsedForThePath = new List<TileScript>();
    public TileScript tilesEnd;
    private void Awake()
    {
        GameManager.managerGame.systemPathfinding = this;
    }
    [SerializeField] private TileScript m_originTile;
    public TileScript originTile
    {
        get
        {
            return m_originTile;
        }
        set
        {
            m_originTile = value;
        }
    }
    [SerializeField] private TileScript m_destTile;
    public TileScript destTile
    {
        get
        {
            return m_destTile;
        }
        set
        {
            m_destTile = value;
        }
    }
    [SerializeField] private List<TileScript> m_tiles;
    public List<TileScript> tiles
    {
        get { return m_tiles; }
        set {
           
            m_tiles = value;
        }
    }
    private GameObject[] tilesArray;

    private void Start()
    {
        tilesArray = GameObject.FindGameObjectsWithTag("Tile");
        TilesLoop();
    }
    private void TilesLoop()
    {
        for (int i = 0; i < tilesArray.Length; i++)
        {
            tiles.Add(tilesArray[i].GetComponent<TileScript>());
        }
    }
    public void StartWithThePathfinding()
    {
        foreach (TileScript tile in m_tiles)
        {
            tile.hCost = Vector3.Distance(tile.transform.position, m_originTile.transform.position);
            tile.gCost = Vector3.Distance(tile.transform.position, m_destTile.transform.position);
            tile.fCost = tile.gCost - tile.hCost;
            tile.UpdateTheInfo();
        }

       m_originTile.ActivateThisTile();
    }

    public void ReestructurePathfinding(Vector3 currentCoordinates)
    {
        
        foreach (TileScript tile in m_tiles)
        {
            tile.hCost = Mathf.Sqrt(Vector3.Distance(currentCoordinates, m_originTile.transform.position));
            tile.gCost =   Mathf.Sqrt(Vector3.Distance(tile.transform.position, m_destTile.transform.position));
            tile.fCost = tile.gCost - tile.hCost;

            if (tile.obstacleValueBool)
            {
                tile.fCost += tile.obstacleValue;
            }
            tile.UpdateTheInfo();
        }
    }

   
    public void ChooseStarterPath()
    {
        m_destTile.ChooseThePathMoreShort();

    }

    public void ResetPathfinding()
    {
        tilesUsedForThePath.Clear();
        destTile = null;
        GameManager.managerGame.clickMouse.DestPoint();
        foreach (TileScript t in tiles)
        {
            t.ResetInfoTiles();
        }
        GameManager.managerGame.characterControl.RayDest();
    }
}
