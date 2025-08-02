using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseMode : MonoBehaviour
{

    public void Next(string NameScene)
    {

        SceneManager.LoadScene(NameScene);

    }

    public void Returns()
    {

        SceneManager.LoadScene("Menu");

    }

}
