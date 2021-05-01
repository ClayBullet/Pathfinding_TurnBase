using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    public TargetType typeTarget;
    public bool actualTargetBool;
}
public enum TargetType
{
    Enemy,
    Allie
}