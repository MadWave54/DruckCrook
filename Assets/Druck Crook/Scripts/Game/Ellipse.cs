using UnityEngine;

public class Ellipse : MonoBehaviour
{

    public bool UsedEllipse, isDisposable, isMore, isFinish;

    public void Used()
    {

        if (!isDisposable)
        {

            isDisposable = true;

            GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 128);

            GetComponent<Collider2D>().enabled = false;

        }

    }

    public void InPlatform()
    {

        if (isDisposable)
        {

            isDisposable = false;

            GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

            GetComponent<Collider2D>().enabled = true;

        }

    }

    public void UsedEllipseByBot()
    {

        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 128);

    }

    public void BotInPlatform()
    {

        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

    }

}
