using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTarget : MonoBehaviour
{
    [SerializeField] SOGridValues gridValues;

    public delegate void ChooseTargetFinnished();
    public static ChooseTargetFinnished chooseTargetFinnished;

    void OnEnable()
    {
        GridGenerator.gridGeneratorFinnished += TargetLocation;
    }

    void TargetLocation()
    {
        int aux = (gridValues.xLength - 1) * gridValues.yLength;
        gridValues.targetPosition = Random.Range(aux, aux + (gridValues.yLength - 1));
        gridValues.currentPlayerPos = Random.Range(0, aux);

        GridGenerator.used[gridValues.targetPosition].Item1.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        GridGenerator.used[gridValues.currentPlayerPos].Item1.GetComponent<Renderer>().material.SetColor("_Color", Color.green);

        gridValues.stopGame = false;
        chooseTargetFinnished?.Invoke();
    }

    private void OnDisable()
    {
        GridGenerator.gridGeneratorFinnished -= TargetLocation;
    }

}
