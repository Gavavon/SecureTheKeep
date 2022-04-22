using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    [Header("Universal")]
    public float transitionTime;

    public static UIControl instance;
    private void Awake()
    {
        instance = this;
    }
    public void OpenScreen(GameObject screen)
    {
        screen.SetActive(true);
        DOTween.To(() => screen.GetComponent<CanvasGroup>().alpha, x => screen.GetComponent<CanvasGroup>().alpha = x, 1, transitionTime);
    }
    public void CloseScreen(GameObject screen)
    {
        DOTween.To(() => screen.GetComponent<CanvasGroup>().alpha, x => screen.GetComponent<CanvasGroup>().alpha = x, 0, transitionTime/5).OnComplete(() => { screen.SetActive(false); });
    }
    public void pressExit()
    {
        Application.Quit();
    }
}
