using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    private static RestartButton instance;

    public void Awake()
    {
        instance = this;
        Hide();
    }

    public static void Show()
    {
        instance.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        instance.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        GameObject.Find("Player").GetComponent<PlayerController>().ReturnToNormalSpeed();
    }
}