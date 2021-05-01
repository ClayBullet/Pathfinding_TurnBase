using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCharacter : MonoBehaviour
{
    [Header("ACTUAL CONTROL CHARACTER")]
    [Space]
    public GameObject currentCharacter;
    public GameObject exampleCharacter;
    [SerializeField] private LayerMask maskLayerWithoutNPCLayer;
    public List<GameObject> controlablesCharacters = new List<GameObject>();
    private void Awake()
    {
        GameManager.managerGame.characterControl = this;
    }
    private void Start()
    {
        RayDest();
        CalculateHowManyPlayersExist();
        GameManager.managerGame.characterAimingSystem.cameraPont = currentCharacter.gameObject.transform.GetChild(1).gameObject;
    }
    public void CalculateHowManyPlayersExist()
    {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("npc");
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].GetComponent<TargetSelector>().typeTarget == TargetType.Allie)
                controlablesCharacters.Add(characters[i]);
        }
    }
    public void RayDest()
    {
        GameManager.managerGame.systemPathfinding.originTile = TilesRay();

    }
    public TileScript TilesRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(currentCharacter.transform.position, Vector3.down, out hit, maskLayerWithoutNPCLayer))
        {
            if (hit.collider.GetComponent<TileScript>() != null) return hit.collider.GetComponent<TileScript>();
        }

        return null;
    }

    public IEnumerator CharacterMovement()
    {
        for (int i = GameManager.managerGame.systemPathfinding.tilesUsedForThePath.Count - 1; i > 0; i--)
        {
            Vector3 currentPosition = GameManager.managerGame.systemPathfinding.tilesUsedForThePath[i].gameObject.transform.position;
            currentCharacter.transform.position = new Vector3(currentPosition.x, currentCharacter.transform.position.y + currentPosition.y, currentPosition.z);
            yield return new WaitForSeconds(.1f);
        }
        Vector3 lastPosition = GameManager.managerGame.systemPathfinding.destTile.transform.position;
        currentCharacter.transform.position = new Vector3(lastPosition.x, transform.position.y + lastPosition.y + 1f, lastPosition.z);
        GameManager.managerGame.clickMouse.DestPoint();
        GameManager.managerGame.systemPathfinding.ResetPathfinding();
    }

    public void EntryForShieldProtection()
    {
        currentCharacter.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ExitForShieldProtection()
    {
        currentCharacter.transform.GetChild(0).gameObject.SetActive(false);

    }

    public void CurrentCharacterForUse(GameObject changeableCharacter)
    {
        currentCharacter = changeableCharacter;
    }
}
