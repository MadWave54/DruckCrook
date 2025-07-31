using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{

    [SerializeField] private Image ImageText;
    [SerializeField] private Sprite[] Texts;

    private int Index;

    private void Update()
    {

        ImageText.sprite = Texts[Index];

    }

    public void Returns()
    {

        SceneManager.LoadScene("Menu");

    }

    public void Next()
    {

        if (Index < Texts.Length - 1)
        {

            Index++;

        }

    }

    public void Back()
    {

        if (Index > 0)
        {

            Index--;

        }

    }

}
