using UnityEngine;

public class HeadsetDetector : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] AudioSource audioSource;
    
    private void OnEnable()
    {
        OVRManager.HMDMounted += OnHMDMounted;
        OVRManager.HMDUnmounted += OnHMDUnmounted;
    }

    private void OnDisable()
    {
        OVRManager.HMDMounted -= OnHMDMounted;
        OVRManager.HMDUnmounted -= OnHMDUnmounted;
    }

    private void OnHMDMounted()
    {
        Time.timeScale = 1f;
        //ScrollWithAudio.instance.PlaySound(true);
    }

    private void OnHMDUnmounted()
    {
       // ObjData.instance.OpenPanel();
        Debug.LogError("Шлем снят");
        Time.timeScale = 0f;
       // ScrollWithAudio.instance.PlaySound(false);
    }
}
