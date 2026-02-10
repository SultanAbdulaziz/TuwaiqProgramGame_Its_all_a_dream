using UnityEngine;

public class Vaulting : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private float ledgeCheckHeight = 1.5f;

    [Header("Climb")]
    [SerializeField] private Vector3 climbOffset = new Vector3(0f, 1.2f, 0.5f);
    [SerializeField] private float climbSpeed = 8f;

    private Rigidbody rb;
    private bool isClimbing;
    private Vector3 climbTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckForLedge();

        if (isClimbing)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                climbTarget,
                climbSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, climbTarget) < 0.05f)
            {
                isClimbing = false;
                rb.isKinematic = false;
            }
        }
    }

    void CheckForLedge()
    {
        if (isClimbing) return;

        Vector3 origin = transform.position;

        // wall check (chest height)
        bool wallHit = Physics.Raycast(
            origin,
            transform.forward,
            wallCheckDistance,
            whatIsGround
        );

        // ledge check (head height, forward)
        bool ledgeHit = Physics.Raycast(
            origin + Vector3.up * ledgeCheckHeight,
            transform.forward,
            wallCheckDistance,
            whatIsGround
        );

        // wall detected but no ledge above = climbable
        if (wallHit && !ledgeHit)
        {
            StartClimb();
        }
    }

    void StartClimb()
    {
        isClimbing = true;
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;

        climbTarget = transform.position
                    + transform.forward * climbOffset.z
                    + Vector3.up * climbOffset.y;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position,
            transform.position + transform.forward * wallCheckDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + Vector3.up * ledgeCheckHeight,
            transform.position + Vector3.up * ledgeCheckHeight + transform.forward * wallCheckDistance);
    }
}
