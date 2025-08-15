using UnityEngine;

public class SoundsSettings : MonoBehaviour
{
    public enum SoundType
    {
        Music,
        SFX
    }

    [SerializeField] private SoundType soundType;

    private AudioSource Audio;

    private void Start()
    {

        Audio = GetComponent<AudioSource>();

        Audio.ignoreListenerPause = true;

    }

    public void Update()
    {

        switch (soundType)
        {

            case SoundType.Music:
                Audio.volume = PlayerPrefs.GetFloat("Music");
                break;

            case SoundType.SFX:
                Audio.volume = PlayerPrefs.GetFloat("SFX");
                break;

        }

    }

}
