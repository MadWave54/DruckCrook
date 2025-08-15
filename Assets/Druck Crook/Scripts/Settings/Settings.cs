using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    [SerializeField] private GameObject resetUI;
    private Animator resetUIAnimator;

    [SerializeField] private Image[] buttonsBackImages;
    [SerializeField] private GameObject[] buttonsSwitchGO;
    [SerializeField] private Sprite[] buttonsBackSprites;

    private void Start()
    {

        resetUIAnimator = resetUI.GetComponent<Animator>();

    }

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

    public void Returns()
    {

        try
        {

            GameObject.FindGameObjectWithTag("SceneSwap").GetComponent<SceneSwap>().NextScene("Menu");

        }
        catch (System.Exception ex)
        {

            SceneManager.LoadScene("Menu");

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

    public void ResetButton()
    {

        StartCoroutine(AnimatorResetUI());

    }

    public void ResetCancel()
    {

        StartCoroutine(AnimatorResetUI());

    }

    public void ResetAccept()
    {

        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("Set", 1);
        PlayerPrefs.SetInt("isBought_0", 1);
        PlayerPrefs.SetFloat("Music", 1);
        PlayerPrefs.SetFloat("SFX", 1);
        PlayerPrefs.SetFloat("Vibration", 1);
        PlayerPrefs.SetInt("TimeInGame", 0);

        PlayerPrefs.Save();

        try
        {

            GameObject.FindGameObjectWithTag("GameTimer").GetComponent<GameTimeTracker>().Restart();

        }
        catch (System.Exception ex)
        {

        }

        StartCoroutine(AnimatorResetUI());

    }

    private IEnumerator AnimatorResetUI()
    {

        bool isUI = false;

        if (!resetUI.active)
        {

            isUI = true;

            resetUI.SetActive(true);

        }

        resetUIAnimator.SetBool("isOpen", !resetUIAnimator.GetBool("isOpen"));

        yield return new WaitForEndOfFrame();
        yield return new WaitWhile(() => resetUIAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f || resetUIAnimator.IsInTransition(0));

        if (resetUI.active && !isUI)
        {

            resetUI.SetActive(false);

        }

    }

}
