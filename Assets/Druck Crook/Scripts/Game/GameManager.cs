using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public bool isLose, isWin;

    public bool isArcade, isCompany;

    public int PlatformsCount;

    [SerializeField] GameObject Plyer, Bot;

    [SerializeField] private GameObject UILose, UIWin;

    [SerializeField] private TextMeshProUGUI textTag;

    [SerializeField] private Animator[] animators;

    [SerializeField] private AudioClip[] Clips;

    private AudioSource SFX;

    private void Start()
    {

        try
        {

            SFX = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();

        }
        catch (System.Exception ex)
        {

        }

        if (isCompany)
        {

            textTag.text = "level " + (PlayerPrefs.GetInt("Level_Int") + 1).ToString();

        }

    }

    public void Lose()
    {

        if (!isLose)
        {

            isLose = true;

            if (SFX != null)
            {

                SFX.clip = Clips[0];
                SFX.Play();

            }

            UILose.SetActive(true);

            Time.timeScale = 0;

        }

    }

    public void Win()
    {

        UIWin.SetActive(true);

        if (!isWin)
        {

            isWin = true;

            if (SFX != null)
            {

                SFX.clip = Clips[1];
                SFX.Play();

            }

            if (isCompany)
            {

                PlayerPrefs.SetInt("Level_Int", PlayerPrefs.GetInt("Level_Int") + 1);

            }

            PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points") + 100);

            PlayerPrefs.Save();

            Time.timeScale = 0;

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

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

    }

    public void MainMenu()
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

    private void Update()
    {

        foreach (var item in animators)
        {

            item.Update(Time.unscaledDeltaTime * 1f);

        }

        if (isArcade)
        {

            float distanceX = Mathf.Abs(Plyer.transform.position.x - Bot.transform.position.x);

            if (distanceX >= 12)
            {

                if (Plyer.transform.position.x > Bot.transform.position.x)
                {

                    Win();

                }

                else if(Plyer.transform.position.x < Bot.transform.position.x)
                {

                    Lose();

                }

            }

        }

    }

}
