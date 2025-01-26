using UnityEngine;

public class SpikeBubble : BubbleMovement
{
    public GameObject littleBubblePrefab;
    private float littleBubbleSpeed = 4.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Reset();
    }

    protected override BUBBLE_TYPE GetBubbleType()
    {
        return BUBBLE_TYPE.SPIKE;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public override void Dead()
    {
        var littleBubble = Instantiate(littleBubblePrefab, transform.position, Quaternion.identity);
        littleBubble.GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * littleBubbleSpeed;
        littleBubble = Instantiate(littleBubblePrefab, transform.position, Quaternion.identity);
        littleBubble.GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * littleBubbleSpeed;
        littleBubble = Instantiate(littleBubblePrefab, transform.position, Quaternion.identity);
        littleBubble.GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * littleBubbleSpeed;
        littleBubble = Instantiate(littleBubblePrefab, transform.position, Quaternion.identity);
        littleBubble.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * littleBubbleSpeed;
        base.Dead();
    }

    public override void Reset()
    {
        bubbleHealth = 4;
        scoreWhenPopped = 0;
        moraleWhenPopped = 0;
        populationWhenPopped = 0;
    }
}
