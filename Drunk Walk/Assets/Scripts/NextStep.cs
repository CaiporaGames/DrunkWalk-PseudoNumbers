using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NextStep : MonoBehaviour
{
    [SerializeField] SOGridValues gridValues;

    int aux;

    private void OnEnable()
    {
        ChooseTarget.chooseTargetFinnished += Step;
    }

    void Step()
    {
        
        if (gridValues.stopGame)
        {
            return;
        }
        ChooseDice();
        if (GridGenerator.used[gridValues.currentPlayerPos].Item2 == 1)//bottom left
        {
            if (aux == 2 || aux == 3)
            {
                ChooseDice();
                StartCoroutine(NextMove());

            }
            else
            {
                NextPosition(aux);
            }
        }
        else if (GridGenerator.used[gridValues.currentPlayerPos].Item2 == 2)//top left
        {
            if (aux == 3 || aux == 4)
            {
                ChooseDice();
                StartCoroutine(NextMove());

            }
            else
            {
                NextPosition(aux);
            }
        }
        else if (GridGenerator.used[gridValues.currentPlayerPos].Item2 == 3)//top right
        {
            if (aux == 1 || aux == 4)
            {
                ChooseDice();
                StartCoroutine(NextMove());

            }
            else
            {
                NextPosition(aux);
            }
        }
        else if (GridGenerator.used[gridValues.currentPlayerPos].Item2 == 4)//bottom right
        {
            if (aux == 1 || aux == 2)
            {
                ChooseDice();
                StartCoroutine(NextMove());

            }
            else
            {
                NextPosition(aux);
            }
        }
        else if (GridGenerator.used[gridValues.currentPlayerPos].Item2 == 5)//left
        {
            if (aux == 3)
            {
                ChooseDice();
                StartCoroutine(NextMove());

            }
            else
            {
                NextPosition(aux);
            }
        }
        else if (GridGenerator.used[gridValues.currentPlayerPos].Item2 == 6)//up
        {
            if (aux == 4)
            {
                ChooseDice();
                StartCoroutine(NextMove());

            }
            else
            {
                NextPosition(aux);
            }
        }
        else if (GridGenerator.used[gridValues.currentPlayerPos].Item2 == 7)//right
        {
            if (aux == 1)
            {
                ChooseDice();
                StartCoroutine(NextMove());

            }
            else
            {
                NextPosition(aux);
            }
        }
        else if (GridGenerator.used[gridValues.currentPlayerPos].Item2 == 8)//down
        {
            if (aux == 2)
            {
                ChooseDice();
                StartCoroutine(NextMove());

            }
            else
            {
                NextPosition(aux);
            }
        }
        else
        {
            NextPosition(aux);
        }     

    }

    void NextPosition(int index)
    {
        if (index == 1)
        {
            ChangeColor();
            gridValues.currentPlayerPos = gridValues.currentPlayerPos + gridValues.yLength;
            GridGenerator.used[gridValues.currentPlayerPos].Item1.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        }
        else if (index == 2)
        {
            ChangeColor();
            gridValues.currentPlayerPos = gridValues.currentPlayerPos - 1;
            GridGenerator.used[gridValues.currentPlayerPos].Item1.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);

        }
        else if (index == 3)
        {
            ChangeColor();
            gridValues.currentPlayerPos = gridValues.currentPlayerPos - gridValues.yLength;
            GridGenerator.used[gridValues.currentPlayerPos].Item1.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);

        }
        else
        {
            ChangeColor();
            gridValues.currentPlayerPos = gridValues.currentPlayerPos + 1;
            GridGenerator.used[gridValues.currentPlayerPos].Item1.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);

        }

        UIManager.Instance.SetStepCounter();
        WinCondition();
        StartCoroutine(NextMove());
    }

    void ChangeColor()
    {
        float color = GridGenerator.used[gridValues.currentPlayerPos].Item3;
        float color1 = GridGenerator.used[gridValues.currentPlayerPos].Item4;
        float color2 = GridGenerator.used[gridValues.currentPlayerPos].Item5;
        color += 0.1f;
        color1 += 0.075f;
        color2 += 0.05f;
        GridGenerator.used[gridValues.currentPlayerPos] = (GridGenerator.used[gridValues.currentPlayerPos].Item1, GridGenerator.used[gridValues.currentPlayerPos].Item2, color, color1, color2);
        GridGenerator.used[gridValues.currentPlayerPos].Item1.GetComponent<Renderer>().material.SetColor("_Color", new Vector4(color1, color, color2, 1));
    }

    IEnumerator NextMove()
    {
        yield return new WaitForSeconds(gridValues.stepTimer);
        Step();
    }

    void ChooseDice()
    {
        if (gridValues.isPerlin)
        {
            aux = PsedoDice();
        }
        else if (gridValues.isPow)
        {
            aux = PsedoDice();
        }
        else if (gridValues.isProduct)
        {
            aux = PsedoDice();
        }
        else if (gridValues.isSin)
        {
            aux = PsedoDice();
        }
        else
        {
            aux = Dice();
        }

    }

    //Four directions in clockwise: Forward = 1, Down = 2, Backward = 3, Up = 4;
    int Dice()
    {
        int rand = UnityEngine.Random.Range(1, 5);
        return rand;
    }

    int PsedoDice()
    {
        int aux;
        float rand = 0;
        if (gridValues.isPerlin)
        {
            rand = Mathf.Clamp01(Mathf.PerlinNoise(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f)));
        }
        else if (gridValues.isPow)
        {

            rand = Mathf.Clamp01(Mathf.Pow(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f))); //x^y
        }
        else if (gridValues.isProduct)
        {

            rand = Mathf.Clamp01(UnityEngine.Random.Range(0.0f, 1.0f) * UnityEngine.Random.Range(0.0f, 1.0f)); //x * y
        }
        else if (gridValues.isSin)
        {

            rand = Mathf.Clamp01((Mathf.Sin(DateTime.Now.Second) + 1 ) * 0.5f);//sin(x)
        }

        Debug.Log(rand);

        if (rand <= 0.25f)
        {
            aux = 1;
            return aux;
        }
        else if(rand <= 0.5f)
        {
            aux = 2;
            return aux;
        }
        else if (rand <= 0.75f)
        {
            aux = 3;
            return aux;
        }
        else
        {
            aux = 4;
            return aux;
        }
       
    }
    void WinCondition()
    {
        if (gridValues.currentPlayerPos == gridValues.targetPosition)
        {
            UIManager.Instance.EndGameText();
            StopAllCoroutines();
        }
    }


    private void OnDisable()
    {
        ChooseTarget.chooseTargetFinnished -= Step;

    }

}
