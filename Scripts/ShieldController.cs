using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private List<Vector3> finalLines = new List<Vector3>();
    public GameObject coverGenerated;
    [SerializeField] private Transform playerObject;
    [SerializeField] private LayerMask maskLayerForDontDetectNPCS;
    private bool coverBoolean;
    private void Awake()
    {
        GameManager.managerGame.controllerShield = this;
    }
    private void Update()
    {
        RayCover();
    }

    private void RayCover()
    {
        for (int i = 0; i < finalLines.Count; i++)
        {
            RaycastHit hit;
            if (Physics.Linecast(playerObject.position, finalLines[i] + playerObject.position, out hit, maskLayerForDontDetectNPCS))
            {
                if (hit.collider.CompareTag("Shield"))
                {
                    coverBoolean = true;
                }
            }
        }

        if (coverBoolean)
        {
            GameManager.managerGame.characterControl.EntryForShieldProtection();
        }
        else
        {
            GameManager.managerGame.characterControl.ExitForShieldProtection();
        }
        coverBoolean = false;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < finalLines.Count; i++)
        {
            Gizmos.DrawRay(playerObject.position, finalLines[i]);
        }
    }
}
