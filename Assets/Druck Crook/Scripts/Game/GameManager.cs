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

    private void Start()
    {

        if (isCompany)
        {

            textTag.text = "level " + (PlayerPrefs.GetInt("Level_Int") + 1).ToString();

        }

    }

    public void Lose()
    {

        isLose = true;

        UILose.SetActive(true);

        Time.timeScale = 0;

    }

    public void Win()
    {

        UIWin.SetActive(true);

        if (!isWin)
        {

            isWin = true;

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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void MainMenu()
    {

        Time.timeScale = 1;

        SceneManager.LoadScene("Menu");

    }

    private void Update()
    {

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
