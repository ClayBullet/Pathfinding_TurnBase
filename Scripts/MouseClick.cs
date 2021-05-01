using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Transform originPoint, destinationPoint;

    private void Awake()
    {
        GameManager.managerGame.clickMouse = this;
    }
    public void DestPoint()
    {
        destinationPoint = null;
    }
    private void LateUpdate()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && Input.GetKeyDown(KeyCode.Mouse0) && !GameManager.managerGame.characterAimingSystem.isAimingBool)
        {
            if (hit.collider.CompareTag("Tile"))
            {
               if (destinationPoint == null)
                {
                   
                    GameManager.managerGame.systemPathfinding.destTile = hit.collider.gameObject.GetComponent<TileScript>();
                    hit.collider.gameObject.GetComponent<TileScript>().ChargeTargetMaterial();

                    destinationPoint = hit.collider.transform;

                    GameManager.managerGame.systemPathfinding.StartWithThePathfinding();

                }
            }
        }    
    }
}
