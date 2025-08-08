using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatingBacks : MonoBehaviour
{

    [SerializeField] private Transform positionsNewBack;
    [SerializeField] private GameObject newBack;

    private SpriteRenderer spriteRenderer;
    private Renderer objectRenderer;
    private Camera mainCamera;

    private Transform Parent;

    private bool isCreat;

    private void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        objectRenderer = GetComponent<Renderer>();
        mainCamera = Camera.main;

        Parent = GameObject.FindGameObjectWithTag("Backs").transform;

    }

    void Update()
    {

        if (!isCreat)
        {

            bool isOnEdgeOrLeft = IsSpriteRightEdgeOnOrLeftOfCamera();

            if (isOnEdgeOrLeft)
            {

                isCreat = true;

                var Back = Instantiate(newBack, positionsNewBack.transform.position, Quaternion.identity, Parent);

                if (!spriteRenderer.flipX)
                {

                    Back.GetComponent<SpriteRenderer>().flipX = true;

                }

                else
                {

                    Back.GetComponent<SpriteRenderer>().flipX = false;

                }

            }

        }

        //bool IsOutside = IsOutsideCamera();

        //if (IsOutside)
        //{

        //    this.gameObject.SetActive(false);

        //}

        //else
        //{

        //    this.gameObject.SetActive(true);

        //}
        
    }

    public bool IsOutsideCamera()
    {

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        bool isVisible = GeometryUtility.TestPlanesAABB(planes, objectRenderer.bounds);

        return !isVisible;

    }

    public bool IsSpriteRightEdgeOnOrLeftOfCamera()
    {

        float spriteRightEdge = spriteRenderer.bounds.max.x;

        float cameraRightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, 0)).x;

        return spriteRightEdge <= cameraRightEdge;

    }

}
