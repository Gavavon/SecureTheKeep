using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChoose : MonoBehaviour
{
    public GameObject[] heads;

    void Start()
    {
        heads[Random.Range(0, heads.Length)].SetActive(true);
    }
}
