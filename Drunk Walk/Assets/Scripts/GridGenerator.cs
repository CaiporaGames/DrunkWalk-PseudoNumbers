using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    
    public static List<(GameObject, int, float, float, float)> used = new List<(GameObject, int, float, float, float)>();

    public delegate void GridGeneratorFinnished();
    public static GridGeneratorFinnished gridGeneratorFinnished;


    [SerializeField] List<GameObject> cubes = new List<GameObject>();
    [SerializeField] SOGridValues gridValues;
    [SerializeField] float maxTime = 0.5f;
    
    
    float time = 0;


    [ContextMenu("Generate")]
    void Awake()
    {
        NewGame();
        UIManager.nextGame += NewGame;
    }  

    void AddToDictionary(GameObject obj, int i, int j)
    {
        if (i == 0 && j == 0)//Bottom left
        {
            used.Add((obj, 1, 0, 0, 0));
        }
        else if (i == 0 && j == (gridValues.yLength-1))//Top left
        {
            used.Add((obj, 2, 0, 0, 0));

        }
        else if (i == (gridValues.xLength-1) && j == (gridValues.yLength-1))//Top right
        {
            used.Add((obj, 3, 0, 0, 0));

        }
        else if (i == (gridValues.xLength-1) && j == 0) //bottom right
        {
            used.Add((obj, 4, 0, 0, 0));

        }
        else if (i == 0 && (0 < j && j < gridValues.yLength-1))//left
        {
            used.Add((obj, 5, 0, 0, 0));

        }
        else if (i == (gridValues.xLength-1) && (0 < j && j < gridValues.yLength-1))//right
        {
            used.Add((obj, 7, 0, 0, 0));

        }
        else if ((0 < i && i < (gridValues.xLength-1)) && j == 0)//bottom
        {
            used.Add((obj, 8, 0, 0, 0));

        }
        else if ((0 < i && i < (gridValues.xLength - 1)) && j % gridValues.yLength == (gridValues.yLength-1))//up
        {
            used.Add((obj, 6, 0, 0, 0));

        }
        else
        {
            used.Add((obj, 9, 0, 0, 0));
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(maxTime);

        gridGeneratorFinnished?.Invoke();

    }

    void NewGame()
    {
        if (used.Count != 0)
        {
            for (int i = 0; i < gridValues.yLength * gridValues.xLength; i++)
            {
                used[i].Item1.GetComponent<Renderer>().material.SetColor("_Color", gridValues.gridColor.color);

                used[i] = (used[i].Item1, 0, 1, 1, 1);
            }

        }
      

        used.Clear();


        for (int i = 0; i < cubes.Count; i++)
        {
            cubes[i].transform.position = new Vector3(0, -5, 0);
        }
        int k = 0;

        for (int i = 0; i < gridValues.xLength; i++)
        {
            for (int j = 0; j < gridValues.yLength; j++)
            {
                cubes[k].GetComponent<Lerping>().Lerp(time, maxTime, gridValues, i, j);
                AddToDictionary(cubes[k], i, j);

                k++;
            }
        }

        StartCoroutine(Timer());
    }

    private void OnDisable()
    {
        UIManager.nextGame -= NewGame;

    }
}
