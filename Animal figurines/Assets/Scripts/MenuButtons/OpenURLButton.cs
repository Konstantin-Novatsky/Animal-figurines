using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public string urlGitHub = "https://github.com/Konstantin-Novatsky/Number-three";
    public string urlTelegram = "https://t.me/Novatsky";

    public void OpenGitHub()
    {
        if (!string.IsNullOrEmpty(urlGitHub))
        {
            Application.OpenURL(urlGitHub);
        }
    }

    public void OpenTelegram()
    {
        if (!string.IsNullOrEmpty(urlTelegram))
        {
            Application.OpenURL(urlTelegram);
        }
    }
}