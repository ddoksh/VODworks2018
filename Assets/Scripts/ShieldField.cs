﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldField : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Erase");

        GameObject go = collision.gameObject;
        if (go.tag == "Enemy")
        {
            Destroy(go);
        }
    }
}