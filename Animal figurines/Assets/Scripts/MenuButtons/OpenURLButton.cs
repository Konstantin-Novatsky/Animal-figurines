using JetBrains.Annotations;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    [SerializeField] [NotNull] private string urlGitHub = "https://github.com/Konstantin-Novatsky/Number-three";
    [SerializeField] [NotNull] private string urlTelegram = "https://t.me/Novatsky";

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