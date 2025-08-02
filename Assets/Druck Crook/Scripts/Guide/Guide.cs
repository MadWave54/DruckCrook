using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{

    [SerializeField] private Image imageText;
    [SerializeField] private Sprite[] texts;

    private int Index;

    private void Update()
    {

        imageText.sprite = texts[Index];

    }

    public void Returns()
    {

        SceneManager.LoadScene("Menu");

    }

    public void Next()
    {

        if (Index < texts.Length - 1)
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
