using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get { return _instance; }}
    public delegate void NextGame();
    public static NextGame nextGame;

    static UIManager _instance;

    [SerializeField] TextMeshProUGUI stepCounter;
    [SerializeField] TextMeshProUGUI speedDisplay;
    [SerializeField] GameObject endGameText;
    [SerializeField] SOGridValues gridValues;
    [SerializeField] Slider speed;
    [SerializeField] Toggle perlinToggle;
    [SerializeField] Toggle powToggle;
    [SerializeField] Toggle productToggle;
    [SerializeField] Toggle sinToggle;

    int count = 1;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        speed.value = gridValues.stepTimer;
        speedDisplay.text = gridValues.stepTimer.ToString();
        perlinToggle.isOn = gridValues.isPerlin;
        productToggle.isOn = gridValues.isProduct;
        sinToggle.isOn = gridValues.isSin;
        powToggle.isOn = gridValues.isPow;

    }

    public void SetStepCounter()
    {
        count += 1;
        stepCounter.text = count.ToString();
    }

    public void EndGameText()
    {
        gridValues.stopGame = true;
        endGameText.SetActive(true);
    }

    public void NewGame()
    {
        count = 0;

        endGameText.SetActive(false);
        nextGame?.Invoke();
    }

    public void PerlinToggle()
    {
        gridValues.isPerlin = perlinToggle.isOn;
    }
    public void PowToggle()
    {
        gridValues.isPow = powToggle.isOn;
    }
    public void SinToggle()
    {
        gridValues.isSin = sinToggle.isOn;
    }
    public void ProductToggle()
    {
        gridValues.isProduct = productToggle.isOn;
    }

    void Update()
    {
        gridValues.stepTimer = speed.value;
        speedDisplay.text = gridValues.stepTimer.ToString("0.00");
    }
}
