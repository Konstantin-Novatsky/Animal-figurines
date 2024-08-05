using UnityEngine;
using UnityEngine.UI;

public class SettingsUseButton : MonoBehaviour
{
    [SerializeField] private Animator settingsAnimator;
    public void OnSettingsButtonClick()
    {
        settingsAnimator.SetBool("isSettingsOpen", !settingsAnimator.GetBool("isSettingsOpen"));
    }
}