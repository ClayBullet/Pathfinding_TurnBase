using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShield : MonoBehaviour
{
    [SerializeField] private bool createTheShieldCoverBool;
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject objectSpawn;
    private GameObject currentSpawnGame;
    private TileScript _tileScript;
    private void Awake()
    {
        _tileScript = GetComponent<TileScript>();
    }
    private void Start()
    {
        if (createTheShieldCoverBool)
        {
            _tileScript.usedTileType = TypeTiles.CoverType;
        }
    }
    [ContextMenu("CREA UN SPAWN")]
    public void AccessToSpawnShield()
    {
        if (currentSpawnGame != null)
            Destroy(currentSpawnGame);
        if (createTheShieldCoverBool)
           currentSpawnGame = Instantiate(objectSpawn, transform.position + offset, Quaternion.identity);
        _tileScript.usedTileType = TypeTiles.CoverType;
    }

    [ContextMenu("DESTRUYE UN SPAWN")]
    public void DestroySpawnShield()
    {
        if (currentSpawnGame != null)
            Destroy(currentSpawnGame);
    }

    
}
