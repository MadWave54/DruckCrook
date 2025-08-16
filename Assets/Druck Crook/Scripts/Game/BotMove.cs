using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMove : MonoBehaviour
{

    [SerializeField] private float jumpForce;

    [SerializeField] private float maxHeight;
    private float startY, startFlyY, PlatformX;

    public GameObject circleInstance, circleInstanceNew;
    private LineRenderer lineRenderer;

    private Rigidbody2D rigidbody;
    private SpringJoint2D joint;


    private float checkDistance = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    private bool isFly, isRope, isReadyToFly;

    private Ellipse ellipseScriptSave;

    private void Start()
    {

        rigidbody = GetComponent<Rigidbody2D>();

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = new Color32(159, 223, 255, 255);
        lineRenderer.endColor = new Color32(159, 223, 255, 255);
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.enabled = false;

    }

    private void Update()
    {

        OnPlatform();

        if (isFly)
        {

            if ((transform.position.y >= startY + maxHeight) && !isRope)
            {

                Rope();

            }

            else if ((transform.position.y >= startFlyY - 0.8f) && isRope && circleInstance.transform.position.x < transform.position.x)
            {

                Fall();

            }

            else if (transform.position.x >= PlatformX)
            {

                rigidbody.velocity = new Vector2(0, 0);

                Fall();

            }

            lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0.02f));
            lineRenderer.SetPosition(1, new Vector3(circleInstance.transform.position.x, circleInstance.transform.position.y, 0.02f));

        }

        if (!isFly && transform.position.y >= startY && circleInstanceNew != null)
        {

            circleInstance = circleInstanceNew;
            circleInstanceNew = null;

            float distanceX = Mathf.Abs(transform.position.x - circleInstance.transform.position.x);

            ellipseScriptSave = circleInstance.GetComponent<Ellipse>();

            if (!ellipseScriptSave.isDisposable && distanceX <= 4 && ellipseScriptSave.isMore)
            {

                Rope();

                isFly = true;

            }

        }

        if (transform.position.y < -5 )
        {

            Camera.main.GetComponent<GameManager>().Win();

        }

    }

    private void Fall()
    {

        ellipseScriptSave = circleInstance.GetComponent<Ellipse>();

        if (ellipseScriptSave.UsedEllipse)
        {

            ellipseScriptSave.UsedEllipseByBot();

        }

        if (ellipseScriptSave.isFinish)
        {

            rigidbody.velocity = new Vector2(0, 0);

        }

        isFly = false;

        isRope = false;

        Destroy(joint);

        lineRenderer.enabled = false;

    }

    private void Jump()
    {

        if (!isFly)
        {

            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);

            isFly = true;

            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }

    }

    private void Rope()
    {

        isRope = true;

        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);

        joint = gameObject.AddComponent<SpringJoint2D>();
        joint.connectedBody = circleInstance.GetComponent<Rigidbody2D>();
        joint.autoConfigureDistance = false;
        joint.distance = Vector2.Distance(transform.position, circleInstance.transform.position);
        joint.dampingRatio = 0.7f;
        joint.frequency = 1.5f;
        joint.enableCollision = false;

        lineRenderer.enabled = true;

        startFlyY = transform.position.y;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (isReadyToFly && collision.tag == "Platform" && !isRope)
        {

            Platform PlatformDB = collision.GetComponent<Platform>();

            if (circleInstance != PlatformDB.Circle && PlatformDB.Circle != null)
            {

                circleInstance = PlatformDB.Circle;

                startY = transform.position.y;

            }

            if (PlatformDB.ThisSample == 5)
            {

                Camera.main.GetComponent<GameManager>().Lose();

            }

            PlatformDB.CreateNewPlatform();

            int multiplier = 1;

            if (PlatformDB.ThisSample == 3)
            {

                multiplier = 2;

            }

            else if (PlatformDB.ThisSample == 4)
            {

                multiplier = 3;

            }

            PlatformX = collision.gameObject.transform.position.x + 4.292f * multiplier;

            Jump();

        }

        else if (!isFly && collision.tag == "Ellipse" && !isRope)
        {

            if (circleInstance != collision.gameObject && collision.transform.position.x > circleInstance.transform.position.x)
            {

                circleInstanceNew = collision.gameObject;

            }

        }

    }

    private void OnPlatform()
    {

        RaycastHit2D hit_0 = Physics2D.Raycast(new Vector2(transform.position.x + 0.22f, transform.position.y - 0.35f), new Vector2(1, -1), checkDistance, groundLayer);
        RaycastHit2D hit_1 = Physics2D.Raycast(new Vector2(transform.position.x - 0.22f, transform.position.y - 0.35f), new Vector2(-1, -1), checkDistance, groundLayer);

        Debug.DrawRay(new Vector2(transform.position.x + 0.22f, transform.position.y - 0.35f), new Vector2(1, -1) * checkDistance, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x - 0.22f, transform.position.y - 0.35f), new Vector2(-1, -1) * checkDistance, Color.red);

        if (hit_0.collider != null)
        {

            Rigidbody2D hit_0Rb = hit_0.collider.attachedRigidbody;

            if (circleInstance != null)
            {

                ellipseScriptSave = circleInstance.GetComponent<Ellipse>();
                ellipseScriptSave.BotInPlatform();

            }

            isReadyToFly = true;

        }

        else if (hit_1.collider != null)
        {

            Rigidbody2D hit_1Rb = hit_1.collider.attachedRigidbody;

            if (circleInstance != null)
            {

                ellipseScriptSave = circleInstance.GetComponent<Ellipse>();
                ellipseScriptSave.BotInPlatform();

            }

            isReadyToFly = true;

        }

        else
        {

            isReadyToFly = false;

        }

    }

}
