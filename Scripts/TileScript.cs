using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileScript : MonoBehaviour
{
    [Header("CURRENT PATHFINDING")]
    [Space]
    [Tooltip("THE COST BETWEEN CURRENT AND START")]
    public float hCost;
    [Tooltip("THE COST BETWEEN CURRENT AND FINAL")]
    public float gCost;
    [Tooltip("THE ADD BETWEEN GCOST AND HCOST")]
    public float fCost;

    public bool isUsedBool;
    [Header("UI TEXT")]
    [Space]
    [SerializeField] private Text hCostText;
    [SerializeField] private Text gCostText;
    [SerializeField] private Text fCostText;
    [Header("MATERIALS")]
    [Space]
    [SerializeField] private Material targetMaterial;
    [SerializeField] private Material untargetMaterial;
    [SerializeField] private Material pathMaterial;
    [SerializeField] private Material obstacleValueMaterial;
    [SerializeField] private Material correctPathMaterial;
    [SerializeField] private Material defaultMat;

    [Header("RAYCAST TO SEND")]
    [Space]
    [SerializeField] private float radiusContact;

    [SerializeField] private TypeTiles _tilesTypes;
    public TypeTiles showTileType
    {
        get { return _tilesTypes; }
    }
    public TypeTiles usedTileType
    {
        get { return _tilesTypes; }
        set { _tilesTypes = value;  }
    }

    public float obstacleValue = 0f;
    public bool obstacleValueBool;
    public bool coverValueBool;
    private bool checkTheCorrectPathBool;
    [SerializeField] private Vector3[] lineRays;
    public bool availablesForCover;
    private void Start()
    {
        if (obstacleValueBool)
        {
            _tilesTypes = TypeTiles.ObstacleType;
        }
       
        else
        {
            _tilesTypes = TypeTiles.NormalType;
        }

        if (coverValueBool)
        {
            _tilesTypes = TypeTiles.CoverType;
            AvailablesCovers();
        }
    }

    private void AvailablesCovers()
    {
        for (int i = 0; i < lineRays.Length; i++)
        {
            RaycastHit[] hit = Physics.RaycastAll(transform.position, lineRays[i], 1f);

            for (int j = 0; j < hit.Length; j++)
            {
                if(hit[j].collider.GetComponent<TileScript>() != this)
                {
                    hit[j].collider.GetComponent<TileScript>().availablesForCover = true;
                }
            }
         
        }
    }

    public bool RayBetweenPlayerAndCover(GameObject playerPosition)
    {
        RaycastHit hit;

        if (Physics.Linecast(transform.position + GameManager.managerGame.managerTile.offsetTile, playerPosition.transform.position, out hit))
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.gameObject == playerPosition)
            {
                
                return true;

            }
            else
                return false;
        }

        return false;
    }
    public void IncrementObstaclueValue()
    {
        this.GetComponent<MeshRenderer>().material = obstacleValueMaterial;

        fCost += obstacleValue;
        _tilesTypes = TypeTiles.ObstacleType;
    }
    public void ChargeTargetMaterial()
    {
        this.GetComponent<MeshRenderer>().material = targetMaterial;
    }

    public void ChargeUnTargetMaterial()
    {
        this.GetComponent<MeshRenderer>().material = untargetMaterial;
    }
    public void ChargePathMaterial()
    {
        this.GetComponent<MeshRenderer>().material = pathMaterial;
    }

    public void CorrectPathMaterial()
    {
        this.GetComponent<MeshRenderer>().material = correctPathMaterial;

    }
    public void AccessToTheTile()
    {
      ActivateThisTile();
    }

    public void ResetInfoTiles()
    {
        isUsedBool = false;
        checkTheCorrectPathBool = false;
        if(!obstacleValueBool)
         this.GetComponent<MeshRenderer>().material = defaultMat;
    }
   
    public void ActivateThisTile()
    {
        if (this != GameManager.managerGame.systemPathfinding.destTile)
        {
            ChargePathMaterial();
            Collider[] collision = Physics.OverlapSphere(transform.position, radiusContact);
          

            List<TileScript> tilesList = new List<TileScript>();
            foreach (Collider col in collision)
            {
                if (col.gameObject.GetComponent<TileScript>() != null && col.gameObject != this)
                {
                    tilesList.Add(col.gameObject.GetComponent<TileScript>());
                }
            }
            TileScript moreShortPosition = null;
            float minimumDistance = Mathf.Infinity;
            foreach (TileScript tile in tilesList)
            {
                if (minimumDistance > tile.fCost && tile != this.GetComponent<TileScript>() && !tile.isUsedBool)
                {
                    moreShortPosition = tile;
                   
                    minimumDistance = tile.fCost;

                }
            }
         

            if (moreShortPosition != null)
            {
                isUsedBool = true;
                GameManager.managerGame.systemPathfinding.ReestructurePathfinding(transform.position);
                moreShortPosition.ChargePathMaterial();
                moreShortPosition.AccessToTheTile();



            }
            else
            {
                GameManager.managerGame.systemPathfinding.originTile.AccessToTheTile();
            }


        }
        else
        {
            
            GameManager.managerGame.systemPathfinding.ChooseStarterPath();
        }

    }

    public void ChooseThePathMoreShort()
    {
        if (this != GameManager.managerGame.systemPathfinding.originTile)
        {
            Collider[] collision = Physics.OverlapSphere(transform.position, radiusContact);


            List<TileScript> tilesList = new List<TileScript>();

            foreach (Collider col in collision)
            {
                if (col.gameObject.GetComponent<TileScript>() != null && col.gameObject != this && col.gameObject.GetComponent<TileScript>().isUsedBool && !col.GetComponent<TileScript>().checkTheCorrectPathBool)
                {
                    tilesList.Add(col.gameObject.GetComponent<TileScript>());
                }
            }

            TileScript moreShortPosition = null;
            float minimumDistance = Mathf.NegativeInfinity;
            
            
            foreach (TileScript tile in tilesList)
            {
                if (minimumDistance < tile.fCost && tile != this.GetComponent<TileScript>() && !tile.checkTheCorrectPathBool)
                {
                    moreShortPosition = tile;

                    minimumDistance = tile.fCost;

                }
            }
           


            if (moreShortPosition != null)
            {

                GameManager.managerGame.systemPathfinding.tilesUsedForThePath.Add(this);

                checkTheCorrectPathBool = true;
                moreShortPosition.CorrectPathMaterial();

                moreShortPosition.ChooseThePathMoreShort();



            }

        }
        else
        {
            StartCoroutine(GameManager.managerGame.characterControl.CharacterMovement());
        }
    }

    public void UpdateTheInfo()
    {
        hCostText.text = "HCOST " + hCost.ToString("F2");
        gCostText.text = "GCOST " + gCost.ToString("F2");
        fCostText.text = "FCOST " + fCost.ToString("F2");
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + GameManager.managerGame.managerTile.offsetTile, GameManager.managerGame.characterControl.exampleCharacter.transform.position);
    }
}
public enum TypeTiles
{
    NormalType,
    ObstacleType,
    CoverType,
    AvailableCoverType
}