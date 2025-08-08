using UnityEngine;
using UnityEngine.EventSystems;

public class GameJoystick : MonoBehaviour
{

    [SerializeField] private float jumpForce;

    [SerializeField] private float maxHeight;
    private float startY;

    private GameObject circleInstance;
    private LineRenderer lineRenderer;

    private Rigidbody2D rigidbody;
    private SpringJoint2D joint;

    private Camera mainCamera;
    private Renderer objectRenderer;

    private float checkDistance = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    private bool isFly, isRope, isReadyToFly;

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

    }

    private void Update()
    {

        isReadyToFly = IsStandingOn();

        if (Input.GetMouseButtonDown(0) && isReadyToFly && !IsPointerOverUI())
        {

            Jump();

        }

        else if (Input.GetMouseButtonDown(0) && isRope && !IsPointerOverUI())
        {

            Fall();

        }

        if (isFly)
        {

            if (transform.position.y >= startY + maxHeight)
            {

                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);

                if (!isRope)
                {

                    Rope();

                }

            }

            lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.01f));
            lineRenderer.SetPosition(1, new Vector3(circleInstance.transform.position.x, circleInstance.transform.position.y, circleInstance.transform.position.z + 0.01f));

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

        isFly = false;

        isRope = false;

        Destroy(joint);

        lineRenderer.enabled = false;

    }

    private void Jump()
    {

        isFly = true;

        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }

    private void Rope()
    {

        isRope = true;

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

                PlatformDB.CreatePlatform();

            }

        }

    }

    public bool IsOutsideCamera()
    {

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        bool isVisible = GeometryUtility.TestPlanesAABB(planes, objectRenderer.bounds);

        return !isVisible;

    }

    bool IsStandingOn()
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
