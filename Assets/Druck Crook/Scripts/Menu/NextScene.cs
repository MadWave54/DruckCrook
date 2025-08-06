using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
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

}
