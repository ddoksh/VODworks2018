using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static float speed = 6f;

    public float levelSpeed = 6f;

    public Vector3 dir = Vector3.zero;

    private void Start()
    {
        ChangeTarget("Player");
        //speed = levelSpeed; <- 잘못된 코드
    }

    private void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    public void ChangeTarget(string newTargetName)
    {
        Vector3 gapPos = GameObject.FindWithTag(newTargetName).transform.position - transform.position;
        dir = gapPos.normalized;
    }

    public void ChangeE_Speed(float slowSpeed)
    {
        speed = slowSpeed;
    }

    public void ReturnE_Speed()
    {
        speed = levelSpeed;
    }
}