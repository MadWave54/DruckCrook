using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Guide : MonoBehaviour
{

    [SerializeField] private Image imageText;
    [SerializeField] private Sprite[] texts;

    [SerializeField] private TextMeshProUGUI Info;

    [SerializeField] private RawImage Video;
    [SerializeField] private RenderTexture[] VideoTextures;

    private string[] stringText;

    private int Index;

    private void Start()
    {

        stringText = new string[5];

        stringText[0] = "1.Press to jump.\n2.Release to grab onto the circle.\n3.Press again to unhook.";
        stringText[1] = "1.Release at the right moment — speed is more important than height!\n2.Use swings to build up maximum momentum before jumping.";
        stringText[2] = "1.Watch out for falling platforms and disappearing hooks.";
        stringText[3] = "1.Some hooks vanish after use. 2.Some platforms disappear after use.";
        stringText[4] = "1.Don't fall down\n2.Get to the finish line as quickly as possible.";

    }

    private void Update()
    {

        imageText.sprite = texts[Index];

        Info.text = stringText[Index];

        Video.texture = VideoTextures[Index];

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
