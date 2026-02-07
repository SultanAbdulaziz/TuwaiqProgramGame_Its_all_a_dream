using UnityEngine;

public class Headbob : MonoBehaviour
{
    public float bobSpeed = 6f;
    public float bobAmount = 0.05f;
    private Vector3 startPos;
    private float timer;
    void Start()
    {
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if(Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            timer += Time.deltaTime * bobSpeed;
            float bobOffset = Mathf.Sin(timer) * bobAmount;
            transform.localPosition = startPos + new Vector3(0,bobOffset,0);
        }
        else
        {
            timer = 0;
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, Time.deltaTime * 5f);
        }
    }
}
