using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDMagicBars : MonoBehaviour
{
    public Image bar;

    public enum BarType 
    {
        fire,
        ice,
        vine,
        earth
    }
    public BarType currentBarType = BarType.fire;

    public void Update()
    {
        switch (currentBarType) 
        {
            case BarType.fire:
                bar.fillAmount = (Time.time - PlayerShoot.instance.fireBallStartTime) / PlayerShoot.instance.fireBallResetTime;
                break;
            case BarType.ice:
                bar.fillAmount = (Time.time - PlayerShoot.instance.iceBallStartTime) / PlayerShoot.instance.iceBallResetTime;
                break;
            case BarType.vine:
                bar.fillAmount = (Time.time - PlayerShoot.instance.vineBallStartTime) / PlayerShoot.instance.vineBallResetTime;
                break;
            case BarType.earth:
                bar.fillAmount = (Time.time - PlayerShoot.instance.earthBallStartTime) / PlayerShoot.instance.earthBallResetTime;
                break;
        }
        
        
    }
}
