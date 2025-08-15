using TMPro;
using UnityEngine;

public class AnalyticsShow : MonoBehaviour
{

    public enum GameMode { Company, Arcade, Train, None }

    [SerializeField] TextMeshProUGUI[] Texts_Analytics;

    private void Update()
    {

        if (PlayerPrefs.GetInt("Successful_Holds_Count") > 0)
        {

            string Red = (PlayerPrefs.GetInt("Successful_Holds_Count") - PlayerPrefs.GetInt("Successful_Holds_Good")).ToString();

            Texts_Analytics[0].text = "<color=green>" + PlayerPrefs.GetInt("Successful_Holds_Good").ToString() + "</color>" + "/" + "<color=red>" + Red + "</color>";

        }

        else
        {

            Texts_Analytics[0].text = "-";

        }

        if (PlayerPrefs.GetInt("TimeInGame") > 0)
        {

            Texts_Analytics[1].text = PlayerPrefs.GetInt("TimeInGame").ToString() + "min";

        }

        else
        {

            Texts_Analytics[1].text = "-";

        }

        if (PlayerPrefs.GetInt("Favorite_Mode_Count") > 0)
        {

            int company = PlayerPrefs.GetInt("Favorite_Mode_Company");
            int arcade = PlayerPrefs.GetInt("Favorite_Mode_Arcade");
            int train = PlayerPrefs.GetInt("Favorite_Mode_Train");

            int maxValue = Mathf.Max(company, arcade, train);

            GameMode favoriteMode = GameMode.None;

            if (maxValue == company) favoriteMode = GameMode.Company;
            if (maxValue == arcade && maxValue >= company) favoriteMode = GameMode.Arcade;
            if (maxValue == train && maxValue >= arcade && maxValue >= company) favoriteMode = GameMode.Train;

            Texts_Analytics[2].text = favoriteMode.ToString();

        }

        else
        {

            Texts_Analytics[2].text = "-";

        }

    }

}
