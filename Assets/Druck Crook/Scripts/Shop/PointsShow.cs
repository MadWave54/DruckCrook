using UnityEngine;
using TMPro;

public class PointsShow : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textCount;

    void Update()
    {

        textCount.text = PlayerPrefs.GetInt("Points").ToString();

    }

}
