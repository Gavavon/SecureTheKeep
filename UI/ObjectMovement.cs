using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ObjectMovement : MonoBehaviour
{
    public Transform gameObj;
    public Vector3 gameObjOnScreen;
    public Vector3 gameObjOffScreen;
    public bool showgameObj;
    public float gameObjMoveDuration;

    public enum gameObjState
    {
        OnScreen,
        OffScreen
    }
    public gameObjState currentgameObjState;
    void Start()
    {
        gameObj.localPosition = gameObjOffScreen;
        currentgameObjState = gameObjState.OffScreen;
    }
    void ChoiceShow()
    {
        switch (showgameObj)
        {
            case true:
                if (currentgameObjState != gameObjState.OnScreen)
                {
                    gameObj.DOLocalMove(gameObjOnScreen, gameObjMoveDuration, false);
                    currentgameObjState = gameObjState.OnScreen;
                }
                break;
            case false:
                if (currentgameObjState != gameObjState.OffScreen)
                {
                    gameObj.DOLocalMove(gameObjOffScreen, gameObjMoveDuration, false);
                    currentgameObjState = gameObjState.OffScreen;
                }
                break;
        }
    }
    private void FixedUpdate() 
    {
        ChoiceShow();
    }
}
