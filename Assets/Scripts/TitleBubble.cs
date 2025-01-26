using UnityEngine;

public class TitleBubble : BubbleMovement
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Reset();
    }

    protected override BUBBLE_TYPE GetBubbleType()
    {
        return BUBBLE_TYPE.TITLE;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public new void Reset()
    {
        bubbleHealth = 0;
        scoreWhenPopped = 0;
        moraleWhenPopped = 0;
        populationWhenPopped = 0;
    }
}
