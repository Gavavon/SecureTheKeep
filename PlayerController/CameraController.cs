using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform[] pos;

    public enum CameraSide
    {
        Right,
        Left
    }
    public CameraSide currentCameraSide;

    public static CameraController instance;
    private void Awake()
    {
        instance = this;
    }

    public void switchCamera() 
    {
        switch (currentCameraSide)
        {
            case CameraSide.Right:
                currentCameraSide = CameraSide.Left;
                break;
            case CameraSide.Left:
                currentCameraSide = CameraSide.Right;
                break;
        }
        cameraPos();
    }
    public void cameraPos() 
    {
        switch (currentCameraSide)
        {
            case CameraSide.Right:
                transform.position = pos[0].position;
                break;
            case CameraSide.Left:
                transform.position = pos[1].position;
                break;
        }
    }
}
