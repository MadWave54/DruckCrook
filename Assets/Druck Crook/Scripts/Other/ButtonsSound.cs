using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsSound : MonoBehaviour
{

    [SerializeField] private AudioClip button;

    private AudioSource audio;

    private void Start()
    {

        try
        {

            audio = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();

        }
        catch (System.Exception ex)
        {

        }

    }

    public void ButtonOn()
    {

        if (audio != null)
        {

            audio.clip = button;
            audio.Play();

        }

        if (PlayerPrefs.GetInt("Vibration") == 1)
        {

            Handheld.Vibrate();

        }

    }

}
