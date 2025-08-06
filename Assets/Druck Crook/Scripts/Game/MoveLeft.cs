using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    [SerializeField] private float speed;

    private void Update()
    {

        transform.Translate(speed * Vector2.left * Time.deltaTime, Space.World);

    }

}
