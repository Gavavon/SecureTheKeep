using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameplayManager : MonoBehaviour
{
    public static bool playTutorial;
    public bool isGameOver;

    [Header("Gates")]
    public GateMechanics gate1;
    public GateMechanics gate2;


    [Header("HUD and GameOver Screen")]
    public CanvasGroup hudScreen;
    public CanvasGroup gameOverScreen;
    public GameObject hudScreenObj;
    public GameObject gameOverScreenObj;
    public TMPro.TMP_Text currentWaveText;
    public float transitionTime;
    public static GameplayManager instance;

    [Header("Text")]
    public ObjectMovement textBox;

    private void Awake()
    {
        instance = this;
        try
        {
            playTutorial = GameManagement.instance.getplayTutorial(); 
        }
        catch 
        {
        }
    }

    public bool getPlayTutorial() 
    {
        return playTutorial;
    }

    private void FixedUpdate()
	{
        if (gameOver()) 
        {
            endGame();
        }
	}
    public void endGame() 
    {
        if (textBox.showgameObj) 
        {
            textBox.showgameObj = false;
        }
        gameOverScreenObj.SetActive(true);
        currentWaveText.SetText("You Surivived " + WaveManager.instance.getWaveNumber() + " Waves");
        Cursor.lockState = CursorLockMode.None;
        isGameOver = true;
        WaveManager.instance.resetWaveManager();
        try
        {
            DOTween.To(() => gameOverScreen.alpha, x => gameOverScreen.alpha = x, 1, transitionTime);
            DOTween.To(() => hudScreen.alpha, x => hudScreen.alpha = x, 0, transitionTime).OnComplete(() => { hudScreenObj.SetActive(false); });
        }
        catch { }
    }
    public void forceEndGame() 
    {
        endGame();
    }
    public bool gameOver() 
    {
        if (gate2.currentGateHealth <= 0 && gate1.currentGateHealth <= 0)
        {
            return true;
        }
        return false;
    }

}
