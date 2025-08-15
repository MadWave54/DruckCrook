using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    [SerializeField] private GameObject pauseUI;
    [SerializeField] private Animator animatorPauseUI, animatorTag;

    [SerializeField] private GameObject[] tags;

    [SerializeField] private Image icon;
    [SerializeField] private Sprite[] iconSprites;

    [SerializeField] private Image[] buttonsBackImages;
    [SerializeField] private GameObject[] buttonsSwitchGO;
    [SerializeField] private Sprite[] buttonsBackSprites;

    private bool isOpen;

    private void Update()
    {

        for (int i = 0; i < 3; i++)
        {

            if (i == 0)
            {

                if (PlayerPrefs.GetFloat("Music") == 0)
                {

                    buttonsBackImages[0].sprite = buttonsBackSprites[0];

                    buttonsSwitchGO[0].SetActive(false);
                    buttonsSwitchGO[1].SetActive(true);

                }

                else
                {

                    buttonsBackImages[0].sprite = buttonsBackSprites[1];

                    buttonsSwitchGO[0].SetActive(true);
                    buttonsSwitchGO[1].SetActive(false);

                }

            }

            else if (i == 1)
            {

                if (PlayerPrefs.GetFloat("SFX") == 0)
                {

                    buttonsBackImages[1].sprite = buttonsBackSprites[0];

                    buttonsSwitchGO[2].SetActive(false);
                    buttonsSwitchGO[3].SetActive(true);

                }

                else
                {

                    buttonsBackImages[1].sprite = buttonsBackSprites[1];

                    buttonsSwitchGO[2].SetActive(true);
                    buttonsSwitchGO[3].SetActive(false);

                }

            }

            else
            {

                if (PlayerPrefs.GetFloat("Vibration") == 0)
                {

                    buttonsBackImages[2].sprite = buttonsBackSprites[0];

                    buttonsSwitchGO[4].SetActive(false);
                    buttonsSwitchGO[5].SetActive(true);

                }

                else
                {

                    buttonsBackImages[2].sprite = buttonsBackSprites[1];

                    buttonsSwitchGO[4].SetActive(true);
                    buttonsSwitchGO[5].SetActive(false);

                }

            }

        }

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

    public void OpenOrClose()
    {

        if (!isOpen)
        {

            StartCoroutine(AnimatorResetUI());

            //tags[0].SetActive(false);
            //tags[1].SetActive(true);

            //pauseUI.SetActive(true);

            icon.sprite = iconSprites[1];

            Time.timeScale = 0;

            //StartCoroutine(AnimatorResetUI);

            isOpen = true;

        }

        else if (isOpen)
        {

            StartCoroutine(AnimatorResetUI());

            tags[0].SetActive(true);
            tags[1].SetActive(false);

            //pauseUI.SetActive(false);

            icon.sprite = iconSprites[0];

            Time.timeScale = 1;

            isOpen = false;

        }

    }

    public void Restart()
    {

        Time.timeScale = 1;

        try
        {

            GameObject.FindGameObjectWithTag("SceneSwap").GetComponent<SceneSwap>().NextScene(SceneManager.GetActiveScene().name);

        }
        catch (System.Exception ex)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

    }

    public void Menu()
    {

        Time.timeScale = 1;

        try
        {

            GameObject.FindGameObjectWithTag("SceneSwap").GetComponent<SceneSwap>().NextScene("Menu");

        }
        catch (System.Exception ex)
        {

            SceneManager.LoadScene("Menu");

        }

    }

    private IEnumerator AnimatorResetUI()
    {

        bool isUI = false;

        if (!pauseUI.active)
        {

            isUI = true;

            pauseUI.SetActive(true);

            animatorTag.SetBool("isOpen", !animatorTag.GetBool("isOpen"));

            yield return new WaitForEndOfFrame();
            yield return new WaitWhile(() => animatorTag.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f || animatorTag.IsInTransition(0));

            tags[0].SetActive(false);
            tags[1].SetActive(true);

        }

        animatorPauseUI.SetBool("isOpen", !animatorPauseUI.GetBool("isOpen"));

        yield return new WaitForEndOfFrame();
        yield return new WaitWhile(() => animatorPauseUI.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f || animatorPauseUI.IsInTransition(0));

        if (pauseUI.active && !isUI)
        {

            pauseUI.SetActive(false);
            tags[1].SetActive(false);
            tags[0].SetActive(true);

            animatorTag.SetBool("isOpen", !animatorTag.GetBool("isOpen"));

            yield return new WaitForEndOfFrame();
            yield return new WaitWhile(() => animatorTag.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f || animatorTag.IsInTransition(0));

        }

    }

}
