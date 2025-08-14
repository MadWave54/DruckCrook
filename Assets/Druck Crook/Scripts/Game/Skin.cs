using UnityEngine;

public class Skin : MonoBehaviour
{

    [SerializeField] private Sprite[] scinsSprite;

    private SpriteRenderer PlayerSR;

    private void Awake()
    {

        PlayerSR = GetComponent<SpriteRenderer>();

        PlayerSR.sprite = scinsSprite[PlayerPrefs.GetInt("SelectedNow")];

    }

}
