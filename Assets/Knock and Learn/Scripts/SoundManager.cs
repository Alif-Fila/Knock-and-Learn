using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource audioSource; // single AudioSource for SFX
    public AudioClip yayClip;
    public AudioClip awwClip;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayYay() => audioSource.PlayOneShot(yayClip);
    public void PlayAww() => audioSource.PlayOneShot(awwClip);
}
