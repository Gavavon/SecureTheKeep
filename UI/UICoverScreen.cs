using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICoverScreen : MonoBehaviour
{
    public GameObject coverScreen;
    public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(showCoverScreen());
    }
    IEnumerator showCoverScreen() 
    {
        yield return new WaitForSeconds(.5f);
        UIControl.instance.CloseScreen(coverScreen);
        UIControl.instance.OpenScreen(mainMenu);
    }
}
