using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PreparingManager : MonoBehaviour
{

    [SerializeField] private float WaitTime;
    [SerializeField] private TextMeshProUGUI WaitPercentText;

    private void Start()
    {

        StartCoroutine(TextAnimation());

    }

    private IEnumerator TextAnimation()
    {

        int Percent = 0;

        while (Percent < 100)
        {

            yield return new WaitForSeconds(WaitTime / 100);

            Percent += 1;

            WaitPercentText.text = Percent.ToString() + "%";

        }

        if (PlayerPrefs.GetInt("Set") == 0)
        {

            PlayerPrefs.SetInt("Set", 1);
            PlayerPrefs.SetInt("isBought_0", 1);
            PlayerPrefs.SetFloat("Music", 1);
            PlayerPrefs.SetFloat("SFX", 1);
            PlayerPrefs.SetFloat("Vibration", 1);

            PlayerPrefs.Save();

        }

        SceneManager.LoadScene("Menu");

    }

}
