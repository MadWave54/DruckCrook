using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{

    public void Next(string NameScene)
    {

        SceneManager.LoadScene(NameScene);

    }

}
