using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionField : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Erase");

        GameObject go = collision.gameObject;
        if (go.tag == "Enemy")
        {
            Destroy(go);
        }
    }
}