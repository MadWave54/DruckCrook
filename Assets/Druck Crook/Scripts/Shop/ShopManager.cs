using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private Image scin;
    [SerializeField] private Sprite[] scinsSprite;

    [SerializeField] private Image buttonBuyOrSelectImageBack;
    [SerializeField] private Sprite[] buttonBuyOrSelectSpritesBack;
    [SerializeField] private Image buttonBuyOrSelectImageText;
    [SerializeField] private Sprite[] buttonBuyOrSelectTexts;

    [SerializeField] private GameObject costGO;

    [SerializeField] private GameObject iconSwap;
    private Image iconSwapImage;
    private Animator iconSwapAnimator;

    private int indexNow;

    private bool readyToBuy, readyToSelect, readyToSwap;

    private void Start()
    {

        iconSwapAnimator = iconSwap.GetComponent<Animator>();
        iconSwapImage = iconSwap.GetComponent<Image>();

        readyToSwap = true;

    }

    private void Update()
    {

        SetSettingIndex();

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

    public void Next()
    {

        if (indexNow < scinsSprite.Length - 1 && readyToSwap)
        {

            StartCoroutine(SwapAnimation(1, indexNow));

            indexNow++;

        }

    }

    public void Undo()
    {

        if (indexNow > 0 && readyToSwap)
        {

            StartCoroutine(SwapAnimation(2, indexNow));

            indexNow--;

        }

    }

    private void SetSettingIndex()
    {

        scin.sprite = scinsSprite[indexNow];

        string NamePrefs = "isBought_" + indexNow.ToString();

        readyToSelect = false;

        if (PlayerPrefs.GetInt(NamePrefs) == 1)
        {

            if (PlayerPrefs.GetInt("SelectedNow") == indexNow)
            {

                buttonBuyOrSelectImageBack.sprite = buttonBuyOrSelectSpritesBack[0];
                buttonBuyOrSelectImageText.sprite = buttonBuyOrSelectTexts[0];

            }

            else
            {

                buttonBuyOrSelectImageBack.sprite = buttonBuyOrSelectSpritesBack[1];
                buttonBuyOrSelectImageText.sprite = buttonBuyOrSelectTexts[1];

                readyToSelect = true;

            }

            costGO.SetActive(false);

            readyToBuy = false;

        }

        else
        {

            buttonBuyOrSelectImageBack.sprite = buttonBuyOrSelectSpritesBack[2];
            buttonBuyOrSelectImageText.sprite = buttonBuyOrSelectTexts[2];

            costGO.SetActive(true);

            readyToBuy = true;

        }

    }

    public void Buy()
    {

        if (readyToBuy && PlayerPrefs.GetInt("Points") >= 1000)
        {

            string NamePrefs = "isBought_" + indexNow.ToString();

            PlayerPrefs.SetInt(NamePrefs, 1);

            PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points") - 1000);

            PlayerPrefs.Save();

        }

    }

    public void Select()
    {

        if (readyToSelect)
        {

            PlayerPrefs.SetInt("SelectedNow", indexNow);

            PlayerPrefs.Save();

        }

    }

    private IEnumerator SwapAnimation(int DirectionIndex, int indexWas)
    {

        readyToSwap = false;

        iconSwapImage.sprite = scinsSprite[indexWas];

        iconSwap.SetActive(true);

        iconSwapAnimator.SetInteger("Direction", DirectionIndex);

        yield return new WaitForEndOfFrame();
        yield return new WaitWhile(() => iconSwapAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f || iconSwapAnimator.IsInTransition(0));

        iconSwap.SetActive(false);

        iconSwapAnimator.SetInteger("Direction", 0);

        readyToSwap = true;

    }

}
