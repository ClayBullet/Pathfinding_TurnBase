using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSinceCharacter : MonoBehaviour
{
    private GameObject _cameraPoint;
    public GameObject cameraPont
    {
        get{ return _cameraPoint;  }
        set { _cameraPoint = value; }
    }
    [Header("AIM SYSTEM COROUTINE")]
    [Space]
    [SerializeField] private float minimumDistance;
    [SerializeField] private List<GameObject> targetEnemies = new List<GameObject>();

    [SerializeField] private GameObject currentEnemyForAim;
    private int currentNumberFOrAiming;
    public bool isAimingBool;
    private void Awake()
    {
        GameManager.managerGame.characterAimingSystem = this;
    }
    private void Start()
    {
        currentNumberFOrAiming = 0;
        targetEnemies = detectAllTheEnemiesNPCS();
        SelectYourTarget();
    }
    public void AimSinceThisCharacter()
    {
        isAimingBool = true;
        StartCoroutine(AimCharacter(_cameraPoint.transform.position));
    }

    private IEnumerator AimCharacter(Vector3 coordinates)
    {
        Vector3 currentCameraPos = GameManager.managerGame.mainCamera.gameObject.transform.position;
        float currentTime = 0f;
        float disntace = (Mathf.Sqrt(Vector3.Distance(coordinates, currentCameraPos)));

        StartCoroutine(SwitchYourTartget(coordinates, currentEnemyForAim.transform.position));

        while (Mathf.Sqrt(Vector3.Distance(coordinates, currentCameraPos)) > minimumDistance)
        {
            currentTime += Time.deltaTime;
           GameManager.managerGame.mainCamera.transform.position = Vector3.Lerp(currentCameraPos, coordinates, currentTime);
            yield return new WaitForEndOfFrame();
        }

    }

    public void SelectYourTarget()
    {
       currentEnemyForAim = targetEnemies[currentNumberFOrAiming];
    }

  

    private List<GameObject> detectAllTheEnemiesNPCS()
    {
        GameObject[] findAllNPCs = GameObject.FindGameObjectsWithTag("npc");
        List<GameObject> npcEnemies = new List<GameObject>();
        foreach(GameObject npc in findAllNPCs)
        {
            if (npc.GetComponent<TargetSelector>().typeTarget == TargetType.Enemy)
            {
                npcEnemies.Add(npc);
            }
        }
        List<GameObject> correctEnemyOrder = new List<GameObject>();
        float minimumDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
        for (int i = 0; i < npcEnemies.Count; i++)
        {
            minimumDistance = Mathf.Infinity;
            closestEnemy = null;
            for (int j = 0; j < npcEnemies.Count; j++)
            {
                if (!correctEnemyOrder.Contains(npcEnemies[j]))
                {
                    if (Mathf.Sqrt(Vector3.Distance(npcEnemies[j].transform.position, GameManager.managerGame.characterControl.currentCharacter.transform.position)) < minimumDistance)
                    {
                        closestEnemy = npcEnemies[j];
                        minimumDistance = Mathf.Sqrt(Vector3.Distance(npcEnemies[j].transform.position, GameManager.managerGame.characterControl.currentCharacter.transform.position));
                    }
                }
                
            }
            if(closestEnemy != null)
                correctEnemyOrder.Add(closestEnemy);


            closestEnemy = null;
        }
       

        return correctEnemyOrder;
    }
    private IEnumerator SwitchYourTartget(Vector3 oldCoordinates, Vector3 newCoordinates)
    {
       
        float currentTime = 0f;
        while (true)
        {
            currentTime += Time.deltaTime;
            if (currentTime > 1f) break;
            GameManager.managerGame.mainCamera.transform.LookAt(Vector3.Lerp(oldCoordinates, newCoordinates, currentTime));
            yield return new WaitForEndOfFrame();
        }


    }

    public void SelectTargets(bool isRightBool)
    {
        if (isRightBool)
        {
            if(currentNumberFOrAiming < targetEnemies.Count - 1)
                    currentNumberFOrAiming++;
        }
        else
        {
            if(currentNumberFOrAiming > 0)
            currentNumberFOrAiming--;
        }

        SelectYourTarget();
        StartCoroutine(AimCharacter(_cameraPoint.transform.position));

    }

}
