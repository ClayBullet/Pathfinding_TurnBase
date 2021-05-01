using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public void AimSomebody()
    {
        GameManager.managerGame.characterAimingSystem.AimSinceThisCharacter();
    }

}
