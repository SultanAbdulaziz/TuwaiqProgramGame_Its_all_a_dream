using UnityEngine;

public class TurretManager : MonoBehaviour
{
    [Header("Rotation")]
    public float rotationSpeed = 30f;
    public float rotationAngle = 45f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float bulletLifetime = 3f;

    public float fireTimer;
    public float startRotationY;

    public bool turretDown;
    public Animator animator;

    void Start()
    {
        startRotationY = transform.eulerAngles.y;
    }

    void Update()
    {
        if (!GameManager.Instance.timePaused && !turretDown && GameManager.Instance.levelNum == 3)
        {
            RotateTurret();
            HandleShooting();
        }
    }

    void RotateTurret()
    {
        float offset = Mathf.PingPong(Time.time * rotationSpeed, rotationAngle * 2) - rotationAngle;
        transform.rotation = Quaternion.Euler(0f, startRotationY + offset, 0f);
    }

    void HandleShooting()
    {
        if (bulletPrefab == null || firePoint == null)
            return;

        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            Destroy(bullet, bulletLifetime);
            fireTimer = 0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Exit")
        {
            turretDown = true;
            animator.Play("down");
        }
    }
}
