using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimeManager : MonoBehaviour
{
    private Text timeText;

    public void Awake()
    {
        timeText = GetComponent<Text>();
    }

    public void Update()
    {
        timeText.text = "Time:" + Time.timeSinceLevelLoad.ToString("0.0");
    }
}