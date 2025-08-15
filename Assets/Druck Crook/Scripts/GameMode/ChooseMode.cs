using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseMode : MonoBehaviour
{

    public void Next(string NameScene)
    {

        PlayerPrefs.SetInt("Favorite_Mode_Count", PlayerPrefs.GetInt("Favorite_Mode_Count") + 1);

        if (NameScene == "GameCompany")
        {

            PlayerPrefs.SetInt("Favorite_Mode_Company", PlayerPrefs.GetInt("Favorite_Mode_Company") + 1);

        }

        else if (NameScene == "GameArcade")
        {

            PlayerPrefs.SetInt("Favorite_Mode_Arcade", PlayerPrefs.GetInt("Favorite_Mode_Arcade") + 1);

        }

        else if (NameScene == "GameTrain")
        {

            PlayerPrefs.SetInt("Favorite_Mode_Train", PlayerPrefs.GetInt("Favorite_Mode_Train") + 1);

        }

        PlayerPrefs.Save();

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
