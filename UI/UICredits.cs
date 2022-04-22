using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICredits : MonoBehaviour
{
    public Transform credits;
    private Vector3 creditsStartPos;
    public float creditDuration;

    public static UICredits instance;
    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        creditsStartPos = credits.position;

    }
    [ContextMenu("Roll Credits")]
    public void RollCredits()
    {
        credits.transform.DOMove(new Vector3(credits.transform.position.x, credits.transform.position.y + 1500), creditDuration).OnComplete(() => { ResetCredits(); });
    }
    [ContextMenu("Reset Credits")]
    public void ResetCredits()
    {
        DOTween.Kill(credits);
        credits.position = creditsStartPos;

    }

}
/* Special Thanks People
 * Ryan Elliot
 * Totsugeki
 * 
 * 
 */