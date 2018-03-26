using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime = 0.1f;
    public float destroyTime = 5f;
    public float spawnDistance = 10f;

    public IEnumerator Start()
    {
        GameObject enemy = null;
        float degree = 0f;

        for (; ; )
        {
            yield return new WaitForSeconds(spawnTime);
            degree = Random.Range(0f, 360f);
            enemy = GameObject.Instantiate<GameObject>(enemyPrefab, CalcPosition(degree, spawnDistance), Quaternion.identity);
            Destroy(enemy, destroyTime);
        }
    }

    private static Vector3 CalcPosition(float degree, float distance)
    {
        float radian = degree * Mathf.Deg2Rad;

        Vector3 position = Vector3.zero;
        position.x = Mathf.Sin(radian);
        position.y = Mathf.Cos(radian);
        position.Normalize();

        return position * distance;
    }
}