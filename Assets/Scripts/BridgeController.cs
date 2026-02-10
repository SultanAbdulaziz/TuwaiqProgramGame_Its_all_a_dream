using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public float blinkInterval = 0.2f;
    public int blinkCount = 5;
    public float disabledTime = 2f;

    private Renderer bridgeRenderer;
    private Collider bridgeCollider;
    public GameObject placeholder;

    private float timer;
    private int currentBlink;
    private bool isBlinking = true;
    private bool isDisabled = false;

    void Start()
    {
        bridgeRenderer = GetComponent<Renderer>();
        bridgeCollider = GetComponent<Collider>();

        timer = blinkInterval;
        currentBlink = 0;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (isBlinking)
        {
            if (timer <= 0f)
            {
                bridgeRenderer.enabled = !bridgeRenderer.enabled;
                currentBlink++;
                timer = blinkInterval;

                if (currentBlink >= blinkCount * 2)
                {
                    bridgeRenderer.enabled = false;
                    bridgeCollider.enabled = false;
                    placeholder.SetActive(false);
                    isBlinking = false;
                    isDisabled = true;
                    timer = disabledTime;
                }
            }
        }
        else if (isDisabled)
        {
            if (timer <= 0f)
            {
                bridgeRenderer.enabled = true;
                bridgeCollider.enabled = true;
                placeholder.SetActive(true);

                currentBlink = 0;
                isBlinking = true;
                isDisabled = false;
                timer = blinkInterval;
            }
        }
    }
}