using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFireMech : MonoBehaviour
{

    Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        StartCoroutine(lightWorks());

    }
    IEnumerator lightWorks() 
    {
        myLight.intensity = 1f + Mathf.PingPong(Time.time / 2, .6f);
        yield return new WaitForSeconds(.3f);
        switch (Random.Range(0, 1)) 
        {
            case 0:
                myLight.intensity = .8f + Mathf.PingPong(Time.time / 2, .6f);
                break;
            case 1:
                myLight.intensity = 1f + Mathf.PingPong(Time.time / 2, .4f);
                break;
        }
    }
}
