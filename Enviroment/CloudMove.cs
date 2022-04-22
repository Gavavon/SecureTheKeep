using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public int moveSpeed = 10;
    void Update()
    {
        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

        if (gameObject.transform.position.z >= 350)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -350);
        }
    }
}
