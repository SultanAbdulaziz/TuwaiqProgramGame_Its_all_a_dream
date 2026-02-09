using UnityEngine;

public class LaserManager : MonoBehaviour
{
    [Header("Movement Points")]
    public Transform pointA;
    public Transform pointB;

    [Header("Timing")]
    public float moveTime = 1f;

    [Header("Options")]
    public bool playOnStart = true;
    public bool loop = false;

    private float timer;
    private bool isMoving;

    void Start()
    {
        if (playOnStart)
        {
            StartMove();
        }
    }

    void Update()
    {
        if (!isMoving || pointA == null || pointB == null || GameManager.Instance.timePaused)
            return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / moveTime);

        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);

        if (t >= 1f)
        {
            if (loop)
            {
                timer = 0f;
                SwapPoints();
            }
            else
            {
                isMoving = false;
            }
        }
    }

    public void StartMove()
    {
        if (pointA == null || pointB == null)
            return;

        timer = 0f;
        transform.position = pointA.position;
        isMoving = true;
    }

    private void SwapPoints()
    {
        Transform temp = pointA;
        pointA = pointB;
        pointB = temp;
    }
}
