using UnityEngine;

public class SimpleBubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab;
    private float timeBetweenBubbles = 0.25f;
    private float bubbleTimer = 0.0f;
    public DayHandler dayHandler;
    public BubbleMovement.BUBBLE_TYPE bubbleType = BubbleMovement.BUBBLE_TYPE.TITLE;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bubbleTimer -= Time.deltaTime;
        if (bubbleTimer <= 0.0f)
        {
            var bubble = dayHandler.CreateBubble(bubbleType);
            bubble.transform.position = Vector2.zero;
            bubble.SetActive(true);
            bubbleTimer = timeBetweenBubbles;
        }
    }
}
