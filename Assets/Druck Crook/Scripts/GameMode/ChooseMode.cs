using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseMode : MonoBehaviour
{

    public void Next(string NameScene)
    {

        try
        {

            GameObject.FindGameObjectWithTag("SceneSwap").GetComponent<SceneSwap>().NextScene(NameScene);

        }
        catch (System.Exception ex)
        {

            SceneManager.LoadScene(NameScene);

        }

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

}
