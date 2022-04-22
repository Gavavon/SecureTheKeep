using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public enum textSwitch
    {
        on,
        off
    }
    public textSwitch currentTextSwitch;
    public TMP_Text tutorialChecker;
    public Image tutorialImage;


	private void Update()
	{
        switch (currentTextSwitch) 
        {
            case textSwitch.on:
                tutorialChecker.SetText("Tutorial On");
                tutorialImage.color = Color.green;
                GameManagement.instance.updateTutorial(true);
                break;
            case textSwitch.off:
                tutorialChecker.SetText("Tutorial Off");
                tutorialImage.color = Color.red;
                GameManagement.instance.updateTutorial(false);
                break;
        }
    }
    public void textSwitchTrigger() 
    {
        switch (currentTextSwitch)
        {
            case textSwitch.on:
                currentTextSwitch = textSwitch.off;
                break;
            case textSwitch.off:
                currentTextSwitch = textSwitch.on;
                break;
        }
    }
    public void setFPS(int fps) 
    {
        GameManagement.instance.updateFPS(fps);
    }
}
