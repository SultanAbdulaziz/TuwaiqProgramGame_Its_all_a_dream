using UnityEngine;

public class spinbaldemovement : MonoBehaviour
{
    public float distance = 6f;
    public float speed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, distance);

        // «·Õ—ﬂ… ›ﬁÿ ⁄·Ï „ÕÊ— X
        transform.position = new Vector3(
            startPos.x, 
            startPos.y,
            startPos.z+ movement
        );
    }
}
