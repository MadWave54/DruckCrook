using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public GameObject Circle;

    [SerializeField] private Transform Main;
    [SerializeField] private GameObject[] PlatformTemplates;

    private bool isCreate;

    public void CreatePlatform()
    {

        if (!isCreate)
        {

            isCreate = true;

            int Sample = Random.Range(0, PlatformTemplates.Length);

            if (Sample == 0)
            {

                Instantiate(PlatformTemplates[0], new Vector3(Main.position.x + 4.292f, Main.position.y, Main.position.x), Quaternion.identity, GameObject.FindGameObjectWithTag("Platforms").transform);

            }

        }

    }

}
