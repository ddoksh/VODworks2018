using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ScreenEffect : MonoBehaviour
{
    public float speed = 5000f;
    public float overlapSpace = 500f;
    public float marginSpace = 300f;
    public Vector3 dir = Vector3.right;
    public float destroyTime = 3f;

    public void Awake()
    {
        StartCoroutine(Process());
        Destroy(gameObject, destroyTime);
    }

    private IEnumerator Process()
    {
        var rt = GetComponent<RectTransform>();
        var size = Screen.width + overlapSpace;
        rt.anchoredPosition = new Vector2(-size - marginSpace, 0f);
        rt.sizeDelta = new Vector2(size, 0f);

        for (; ; )
        {
            rt.Translate(dir * speed * Time.deltaTime);
            yield return null;
        }
    }
}