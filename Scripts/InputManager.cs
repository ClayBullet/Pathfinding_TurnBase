using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Inputs inputClicker;
    private void Awake()
    {
        GameManager.managerGame.managerInput = this;
    }
}

[System.Serializable] 
public class Inputs
{
    public string nameKey;
    public KeyCode mainKeyCode;
    public KeyCode altKeyCode;
    public bool isNecessaryAxis;
    public Vector2 axis;
    public Inputs(string _nameKey, KeyCode _mainKeyCode, KeyCode _altKeyCode, bool _isNeccesaryAxis, Vector2 _axis)
    {
        _nameKey = nameKey;
        _mainKeyCode = mainKeyCode;
        _altKeyCode = altKeyCode;
        _isNeccesaryAxis = isNecessaryAxis;
    }
}
