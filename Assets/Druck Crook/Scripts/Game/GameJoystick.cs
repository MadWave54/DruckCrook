using UnityEngine;
using UnityEngine.EventSystems;

public class GameJoystick : MonoBehaviour
{

    [SerializeField] private float jumpForce;

    [SerializeField] private float maxHeight;
    private float startY;

    public GameObject circleInstance, circleInstanceNew, circleInstanceWas;
    private LineRenderer lineRenderer;

    private Rigidbody2D rigidbody;
    private SpringJoint2D joint;

    private Camera mainCamera;
    private Renderer objectRenderer;

    private float checkDistance = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    private bool isFly, isRope, isReadyToFly;

    private Ellipse ellipseScriptSave;

    private float timer = 0f;
    private const float secondsInRope = 1;

    private void Start()
    {

        mainCamera = Camera.main;
        objectRenderer = GetComponent<Renderer>();

        rigidbody = GetComponent<Rigidbody2D>();

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = new Color32(159, 223, 255, 255);
        lineRenderer.endColor = new Color32(159, 223, 255, 255);
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.enabled = false;

        isReadyToFly = true;

        circleInstanceWas = null;

    }

    private void Update()
    {

        isReadyToFly = OnPlatform();

        if (isReadyToFly && circleInstance != null)
        {

            ellipseScriptSave = circleInstance.GetComponent<Ellipse>();

            ellipseScriptSave.InPlatform();

        }

        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
        {

            if (isReadyToFly)
            {

                Jump();

            }

            else if (isRope)
            {

                Fall();

            }

            else if (!isFly && transform.position.y >= startY)
            {

                //Debug.Log("NewRope");

                if (circleInstanceNew != null && circleInstanceNew != circleInstanceWas)
                {

                    circleInstanceWas = circleInstance;
                    circleInstance = circleInstanceNew;
                    circleInstanceNew = null;

                    ellipseScriptSave = circleInstance.GetComponent<Ellipse>();

                    float distanceX = Mathf.Abs(transform.position.x - circleInstance.transform.position.x);

                    if (!ellipseScriptSave.isDisposable && distanceX <= 3f)
                    {

                        Rope();

                        isFly = true;

                    }

                }

                else
                {

                    ellipseScriptSave = circleInstance.GetComponent<Ellipse>();

                    float distanceX = Mathf.Abs(transform.position.x - circleInstance.transform.position.x);

                    if (!ellipseScriptSave.isDisposable && distanceX <= 1.8f)
                    {

                        Rope();

                        isFly = true;

                    }

                }

            }

        }

        if (isFly)
        {

            if (((Input.GetMouseButtonUp(0) && !IsPointerOverUI()) || (transform.position.y >= startY + maxHeight)) && !isRope)
            {

                Rope();

            }

            if (isRope)
            {

                timer += Time.deltaTime;

                float boostForce = 0.3f;

                if (timer >= secondsInRope)
                {

                    Vector2 dir = rigidbody.position - (Vector2)circleInstance.transform.position;
                    Vector2 tangent = new Vector2(-dir.y, dir.x).normalized;

                    float side = Mathf.Sign(Vector2.Dot(tangent, rigidbody.velocity));

                    rigidbody.AddForce(tangent * side * boostForce);

                    timer -= secondsInRope;

                }

            }

            lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0.02f));
            lineRenderer.SetPosition(1, new Vector3(circleInstance.transform.position.x, circleInstance.transform.position.y, 0.02f));

        }

        bool IsOutside = IsOutsideCamera();

        if (IsOutside)
        {

            mainCamera.GetComponent<GameManager>().Lose();

            gameObject.SetActive(false);

        }

    }

    private void Fall()
    {

        //Debug.Log("Fall");

        ellipseScriptSave = circleInstance.GetComponent<Ellipse>();

        if (ellipseScriptSave.UsedEllipse)
        {

            ellipseScriptSave.Used();

        }

        isFly = false;

        isRope = false;

        Destroy(joint);

        lineRenderer.enabled = false;

        timer = 0;

    }

    private void Jump()
    {

        //Debug.Log("Jump");

        isFly = true;

        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

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

                Camera.main.GetComponent<GameManager>().Win();

            }

        }

        else if (!isFly && collision.tag == "Ellipse" && !isRope)
        {

            if (circleInstance != collision.gameObject)
            {

                circleInstanceNew = collision.gameObject;

            }

        }

    }

    public bool IsOutsideCamera()
    {

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        bool isVisible = GeometryUtility.TestPlanesAABB(planes, objectRenderer.bounds);

        return !isVisible;

    }

    bool OnPlatform()
    {

        RaycastHit2D hit_0 = Physics2D.Raycast(new Vector2(transform.position.x + 0.22f, transform.position.y - 0.35f), new Vector2(1, -1), checkDistance, groundLayer);
        RaycastHit2D hit_1 = Physics2D.Raycast(new Vector2(transform.position.x - 0.22f, transform.position.y - 0.35f), new Vector2(-1, -1), checkDistance, groundLayer);

        Debug.DrawRay(new Vector2(transform.position.x + 0.22f, transform.position.y - 0.35f), new Vector2(1, -1) * checkDistance, Color.red);
        Debug.DrawRay(new Vector2(transform.position.x - 0.22f, transform.position.y - 0.35f), new Vector2(-1, -1) * checkDistance, Color.red);

        if (hit_0.collider != null)
        {

            Rigidbody2D hit_0Rb = hit_0.collider.attachedRigidbody;


            return hit_0Rb != null;

        }

        if (hit_1.collider != null)
        {

            Rigidbody2D hit_1Rb = hit_1.collider.attachedRigidbody;
            return hit_1Rb != null;

        }

        return false;

    }

    private bool IsPointerOverUI()
    {

        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();

    }

}
