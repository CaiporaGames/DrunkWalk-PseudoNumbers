using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Grid Values", menuName = "Scriptable Objects / Grid Values")]
public class SOGridValues : ScriptableObject
{
    public int xLength = 2;
    public int yLength = 2;
    [Range(1.01f, 1.5f)]
    public float spacing = 2;
    public int currentPlayerPos = 0; 
    public int targetPosition = 0;
    public float stepTimer = 1;
    public bool stopGame = false;
    public Material gridColor;
    public bool isPerlin = false;
    public bool isPow = false;
    public bool isSin = false;
    public bool isProduct = false;

    private void OnDisable()
    {
        stopGame = false;
    }
}
