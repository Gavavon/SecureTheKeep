using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StarterAssets;

public class GameplayInfo : MonoBehaviour
{

    public TMP_Text currentWaveText;
    public TMP_Text currentEnemyNumText;
    public GameObject[] icons;

    // Update is called once per frame
    void Update()
    {
        setHUDText();
        setHUDIcons();
    }
	public void setHUDText()
	{
        currentWaveText.SetText("Current Wave is " + WaveManager.instance.getWaveNumber());
        currentEnemyNumText.SetText(WaveManager.instance.numCurrentEnemies + "/" + WaveManager.instance.totalNumEnemies);
    }
    public void setHUDIcons() 
    {
        switch (StarterAssetsInputs.instance.currentShootType)
        {
            case StarterAssetsInputs.shootType.fire:
                setIcons(0);
                break;
            case StarterAssetsInputs.shootType.ice:
                setIcons(1);
                break;
            case StarterAssetsInputs.shootType.vine:
                setIcons(2);
                break;
            case StarterAssetsInputs.shootType.earth:
                setIcons(3);
                break;
        }
    }
    public void setIcons(int num) 
    {
        for (int i = 0; i < icons.Length; i ++) 
        {
            if (i != num)
            {
                icons[i].SetActive(true);
            }
            else 
            {
                icons[i].SetActive(false);
            }
        }
    }
}
