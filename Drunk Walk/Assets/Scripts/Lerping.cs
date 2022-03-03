using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerping : MonoBehaviour
{
    public void Lerp(float time, float maxTime,SOGridValues gridValues, int i, int j)
    {
        StartCoroutine(Timer(time, maxTime, gridValues, i,j));
    }

    IEnumerator Timer(float time, float maxTime, SOGridValues gridValues, int i, int j)
    {
        float perc = 0;
        while (perc < 1)
        {
            yield return null;
            time = time + Time.deltaTime;
             perc = time / maxTime;
            transform.position = Vector3.Lerp(Vector3.zero, new Vector3(i * gridValues.spacing, 0, j * gridValues.spacing), perc);
        }
    }
}
