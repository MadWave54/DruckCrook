using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSetOnScene : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    private void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;

        Align();

    }

    private void Align()
    {

        float cameraLeftEdge = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, mainCamera.nearClipPlane)).x;

        float spriteWidth = spriteRenderer.bounds.size.x;

        transform.position = new Vector3(
            cameraLeftEdge + spriteWidth / 2,
            transform.position.y,
            transform.position.z
        );

    }

}
