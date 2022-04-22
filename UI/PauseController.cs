using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [Header("Pause Menu")]
    public CanvasGroup pauseScreen;
    public GameObject pauseScreenObj;
    public float pauseTransitionTime;

    #region Pause Menu Components

    [ContextMenu("Pause Screen Transition In")]
    public void PauseScreenTransitionIn()
    {
        pauseScreenObj.SetActive(true);
        DOTween.To(() => pauseScreen.alpha, x => pauseScreen.alpha = x, 1, pauseTransitionTime);
    }
    [ContextMenu("Pause Screen Transition Out")]
    public void PauseScreenTransitionOut()
    {
        DOTween.To(() => pauseScreen.alpha, x => pauseScreen.alpha = x, 0, pauseTransitionTime).OnComplete(() => { pauseScreenObj.SetActive(false); });

    }
    #endregion
}
