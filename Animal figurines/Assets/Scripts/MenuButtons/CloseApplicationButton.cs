using UnityEngine;

public class CloseApplicationButton : MonoBehaviour
{
    public void CloseApplication()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}