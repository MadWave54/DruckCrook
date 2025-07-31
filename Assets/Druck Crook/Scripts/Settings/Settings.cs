using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    [SerializeField] private GameObject ResetUI;

    [SerializeField] private Image[] ButtonsBackImages;
    [SerializeField] private GameObject[] ButtonsSwitchGO;
    [SerializeField] private Sprite[] ButtonsBackSprites;

    private void Update()
    {

        for (int i = 0; i < 3; i++)
        {

            if (i == 0)
            {

                if (PlayerPrefs.GetFloat("Music") == 0)
                {

                    ButtonsBackImages[0].sprite = ButtonsBackSprites[0];

                    ButtonsSwitchGO[0].SetActive(false);
                    ButtonsSwitchGO[1].SetActive(true);

                }

                else
                {

                    ButtonsBackImages[0].sprite = ButtonsBackSprites[1];

                    ButtonsSwitchGO[0].SetActive(true);
                    ButtonsSwitchGO[1].SetActive(false);

                }

            }

            else if (i == 1)
            {

                if (PlayerPrefs.GetFloat("SFX") == 0)
                {

                    ButtonsBackImages[1].sprite = ButtonsBackSprites[0];

                    ButtonsSwitchGO[2].SetActive(false);
                    ButtonsSwitchGO[3].SetActive(true);

                }

                else
                {

                    ButtonsBackImages[1].sprite = ButtonsBackSprites[1];

                    ButtonsSwitchGO[2].SetActive(true);
                    ButtonsSwitchGO[3].SetActive(false);

                }

            }

            else
            {

                if (PlayerPrefs.GetFloat("Vibration") == 0)
                {

                    ButtonsBackImages[2].sprite = ButtonsBackSprites[0];

                    ButtonsSwitchGO[4].SetActive(false);
                    ButtonsSwitchGO[5].SetActive(true);

                }

                else
                {

                    ButtonsBackImages[2].sprite = ButtonsBackSprites[1];

                    ButtonsSwitchGO[4].SetActive(true);
                    ButtonsSwitchGO[5].SetActive(false);

                }

            }

        }

    }

    public void Returns()
    {

        SceneManager.LoadScene("Menu");

    }

    public void Switch(string Name)
    {

        if (Name == "Music")
        {

            Music();

        }

        else if (Name == "SFX")
        {

            SFX();

        }

        else
        {

            Vibration();

        }

    }

    private void Music()
    {

        if (PlayerPrefs.GetFloat("Music") == 0)
        {

            PlayerPrefs.SetFloat("Music", 0.8f);

        }

        else
        {

            PlayerPrefs.SetFloat("Music", 0f);

        }

        PlayerPrefs.Save();

    }

    private void SFX()
    {

        if (PlayerPrefs.GetFloat("SFX") == 0)
        {

            PlayerPrefs.SetFloat("SFX", 1f);

        }

        else
        {

            PlayerPrefs.SetFloat("SFX", 0f);

        }

        PlayerPrefs.Save();

    }

    private void Vibration()
    {

        if (PlayerPrefs.GetFloat("Vibration") == 0)
        {

            PlayerPrefs.SetFloat("Vibration", 1f);

        }

        else
        {

            PlayerPrefs.SetFloat("Vibration", 0f);

        }

        PlayerPrefs.Save();

    }

    public void ResetButton()
    {

        ResetUI.SetActive(true);

    }

    public void ResetCancel()
    {

        ResetUI.SetActive(false);

    }

    public void ResetAccept()
    {

        ResetUI.SetActive(false);

    }

}
