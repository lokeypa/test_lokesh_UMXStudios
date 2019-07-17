using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeItemProperties : MonoBehaviour
{
    public MotionType cubeCurrentMotion = MotionType.right;
    public bool isShortcut = false;
    public Vector2Int exitPoint = Vector2Int.zero; 
    public bool isPitfall = false;
    public bool isInitial = false;
    public bool isFinal = false;
    
}
