using UnityEngine;

public class GameTimeTracker : MonoBehaviour
{

    private float timer = 0f;
    private const float secondsInMinute = 60f;
    private int totalMinutesPlayed = 0;

    private void Start()
    {

        totalMinutesPlayed = PlayerPrefs.GetInt("TimeInGame");

    }

    public void Restart()
    {

        totalMinutesPlayed = PlayerPrefs.GetInt("TimeInGame");

    }

    private void Update()
    {

        timer += Time.deltaTime;

        if (timer >= secondsInMinute)
        {

            totalMinutesPlayed++;

            PlayerPrefs.SetInt("TimeInGame", totalMinutesPlayed);
            PlayerPrefs.Save();

            timer -= secondsInMinute;

        }

    }

    private void OnDestroy()
    {

        int partialMinute = Mathf.FloorToInt(timer / secondsInMinute);

        if (partialMinute > 0)
        {

            totalMinutesPlayed += partialMinute;

            PlayerPrefs.SetInt("TimeInGame", totalMinutesPlayed);
            PlayerPrefs.Save();
            
        }

    }

}