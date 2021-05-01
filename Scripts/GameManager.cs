using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager managerGame;
    [HideInInspector] public PathfindingSystem systemPathfinding;
    [HideInInspector] public InputManager managerInput;
    [HideInInspector] public ControlCharacter characterControl;
    [HideInInspector] public MouseClick clickMouse;
    [HideInInspector] public ShieldController controllerShield;
    [HideInInspector] public AimSinceCharacter characterAimingSystem;
    [HideInInspector] public TileManager managerTile;
    public Camera mainCamera;
    private void Awake()
    {
        managerGame = this;
    }
}
