using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour, IHealthSystem, ISearchAPlace
{
    [Header("ENEMY ORDERS")]
    [Space]
    [SerializeField] private float _radiusForSearch;
    [SerializeField] private EnemyOrders ordersEnemy;
    [SerializeField] private TypeTiles typeTiles;
    public GameObject tileForGo;

    public float maxHealth { get; set; }
    public float currentHealth { get; set ; }


    public float searchRadius { get { return _radiusForSearch; }
                                set { _radiusForSearch = value; } }

    private DetectorBehindFeets _detectBehindMyFeets;

    private void Awake()
    {
        _detectBehindMyFeets = GetComponent<DetectorBehindFeets>();
    }

    private void Start()
    {
        maxHealth = 100f;
        currentHealth = maxHealth;
        
    }
    private void LateUpdate()
    {
        SelectEnemyOrder();
    }

    private void SelectEnemyOrder()
    {
        GameManager.managerGame.characterControl.CurrentCharacterForUse(this.gameObject);
        switch (ordersEnemy)
        {
            case EnemyOrders.AimNPC:
                break;
            case EnemyOrders.SearchCover:
                break;
        }
    }

  
    [ContextMenu("SEARCH A CONCRET PLACE")]
    public void SearchAConcretPlace()
    {
        Collider[] rayColliders = Physics.OverlapSphere(transform.position, _radiusForSearch);

        for (int i = 0; i < rayColliders.Length; i++)
        {
            if (rayColliders[i].CompareTag("Tile"))
            {

                if (rayColliders[i].GetComponent<TileScript>().showTileType == TypeTiles.CoverType)
                {
                   
                            tileForGo = rayColliders[i].gameObject;
                            ChooseTheCover(ref tileForGo);
                        
                    
                
                   
                }
}
        }
    }

  


    public void HealthImplement(float currentHealth)
    {
       
    }

    public void ReceiveDamage(float damageReceived)
    {
       
    }

    public void ChooseTheTarget(TypeTiles typeTile)
    {
        
    }

    public void ChooseTheCover(ref GameObject tileForGO)
    {
        Collider[] rayColliders = Physics.OverlapSphere(tileForGO.transform.position, _radiusForSearch);
       
        foreach (Collider ray in rayColliders)
        {
            if (ray.GetComponent<TileScript>() && ray.GetComponent<TileScript>().availablesForCover)
            {
                for (int j = 0; j < GameManager.managerGame.characterControl.controlablesCharacters.Count; j++)
                {
                    if (!rayColliders[j].GetComponent<TileScript>().RayBetweenPlayerAndCover(GameManager.managerGame.characterControl.controlablesCharacters[j]))
                    {
                        tileForGo = ray.gameObject;
                        break;
                    }
                }  

            }
        }

        GameManager.managerGame.systemPathfinding.originTile = _detectBehindMyFeets.tileBehindOurFeets;
        GameManager.managerGame.systemPathfinding.destTile = tileForGo.GetComponent<TileScript>();
        GameManager.managerGame.systemPathfinding.StartWithThePathfinding();
    }

    public void ChooseTheCover()
    {
        throw new NotImplementedException();
    }

    //private TileScript tileType()
    //{

    //}
}


