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

    [SerializeField] private GameObject CostGO;

    private int indexNow;

    private bool readyToBuy, readyToSelect;

    private void Update()
    {

        SetSettingIndex();

    }

    public void Returns()
    {

        SceneManager.LoadScene("Menu");

    }

    public void Next()
    {

        if (indexNow < scinsSprite.Length - 1)
        {

            indexNow++;

        }

    }

    public void Undo()
    {

        if (indexNow > 0)
        {

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

            CostGO.SetActive(false);

            readyToBuy = false;

        }

        else
        {

            buttonBuyOrSelectImageBack.sprite = buttonBuyOrSelectSpritesBack[2];
            buttonBuyOrSelectImageText.sprite = buttonBuyOrSelectTexts[2];

            CostGO.SetActive(true);

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

}
