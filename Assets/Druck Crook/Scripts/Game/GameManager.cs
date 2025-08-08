using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool isLose;

    [SerializeField] private GameObject UILose;

    public void Lose()
    {

        isLose = true;

        UILose.SetActive(true);

    }

    public void Restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void MainMenu()
    {

        SceneManager.LoadScene("Menu");

    }

}
