using System.Collections;
using UnityEngine;
using TMPro;

public class Platform : MonoBehaviour
{

    public GameObject Circle;

    public int ThisSample;

    [SerializeField] private Transform mainPositions;
    [SerializeField] private GameObject[] platformTemplates;

    [SerializeField] private GameObject prefabsText;
    [SerializeField] private Transform positionsForText;
    private GameObject objectPlatformNext;
    private TextMeshProUGUI textTimer;
    private float floatTimer = 10;
    private bool isTimer;

    private Coroutine timerC;

    private Camera mainCamera;

    private bool isCreate;

    private int multiplier = 1;

    private void Start()
    {

        mainCamera = Camera.main;

    }

    private void Update()
    {

        bool IsInside = IsInsideCamera(gameObject.GetComponent<Renderer>());

        if (IsInside)
        {

            CreateNewPlatform();

        }

        if (isTimer)
        {

            textTimer.transform.position = new Vector3(positionsForText.position.x + 4.292f * multiplier, positionsForText.position.y, positionsForText.position.z);

            bool IsTimerInside = IsInsideCamera(objectPlatformNext.GetComponent<Renderer>());

            if (IsTimerInside && timerC == null)
            {

                timerC = StartCoroutine(Timer());

            }

        }

    }

    public void CreateNewPlatform()
    {

        if (!isCreate && ThisSample != 5)
        {

            isCreate = true;

            if (Camera.main.GetComponent<GameManager>().isCompany)
            {

                int Count = Camera.main.GetComponent<GameManager>().PlatformsCount;

                if (Count < 3 + PlayerPrefs.GetInt("Level_Int"))
                {

                    Camera.main.GetComponent<GameManager>().PlatformsCount++;

                    int Sample = Random.Range(0, platformTemplates.Length - 1);

                    if (ThisSample == 3)
                    {

                        multiplier = 2;

                    }

                    else if (ThisSample == 4)
                    {

                        multiplier = 3;

                    }

                    objectPlatformNext = Instantiate(platformTemplates[Sample], new Vector3(mainPositions.position.x + 4.292f * multiplier, mainPositions.position.y, mainPositions.position.z), Quaternion.identity, GameObject.FindGameObjectWithTag("Platforms").transform).transform.Find("Platform").gameObject;

                    objectPlatformNext.GetComponent<Platform>().ThisSample = Sample;

                    if (Sample == 1)
                    {

                        textTimer = Instantiate(prefabsText, new Vector3(positionsForText.position.x + 4.292f * multiplier, positionsForText.position.y, positionsForText.position.z), Quaternion.identity, GameObject.FindGameObjectWithTag("ForText").transform).GetComponent<TextMeshProUGUI>();

                        isTimer = true;

                    }

                }

                else
                {

                    objectPlatformNext = Instantiate(platformTemplates[5], new Vector3(mainPositions.position.x + 4.292f * multiplier, mainPositions.position.y, mainPositions.position.z), Quaternion.identity, GameObject.FindGameObjectWithTag("Platforms").transform).transform.Find("Platform").gameObject;

                    objectPlatformNext.GetComponent<Platform>().ThisSample = 5;

                }

            }

            else
            {

                int Sample = Random.Range(0, platformTemplates.Length - 1);

                if (ThisSample == 3)
                {

                    multiplier = 2;

                }

                else if (ThisSample == 4)
                {

                    multiplier = 3;

                }

                objectPlatformNext = Instantiate(platformTemplates[Sample], new Vector3(mainPositions.position.x + 4.292f * multiplier, mainPositions.position.y, mainPositions.position.z), Quaternion.identity, GameObject.FindGameObjectWithTag("Platforms").transform).transform.Find("Platform").gameObject;

                objectPlatformNext.GetComponent<Platform>().ThisSample = Sample;

                if (Sample == 1)
                {

                    textTimer = Instantiate(prefabsText, new Vector3(positionsForText.position.x + 4.292f * multiplier, positionsForText.position.y, positionsForText.position.z), Quaternion.identity, GameObject.FindGameObjectWithTag("ForText").transform).GetComponent<TextMeshProUGUI>();

                    isTimer = true;

                }

            }

        }

    }

    public bool IsInsideCamera(Renderer GO)
    {

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        bool isVisible = GeometryUtility.TestPlanesAABB(planes, GO.bounds);

        return isVisible;

    }

    private IEnumerator Timer()
    {

        textTimer.text = floatTimer.ToString();

        yield return new WaitForSeconds(1);

        floatTimer--;

        if (floatTimer < 0)
        {

            textTimer.gameObject.SetActive(false);
            objectPlatformNext.GetComponent<SpriteRenderer>().enabled = false;
            objectPlatformNext.GetComponent<PolygonCollider2D>().enabled = false;

        }

        else
        {

            StartCoroutine(Timer());

        }

    }

}
