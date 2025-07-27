using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Preparing_Manager : MonoBehaviour
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

        SceneManager.LoadScene("Menu");

    }

}
