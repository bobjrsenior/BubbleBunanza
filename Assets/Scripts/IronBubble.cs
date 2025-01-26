using UnityEngine;

public class IronBubble : BubbleMovement
{
    void Start()
    {
        Reset();
    }

    protected override BUBBLE_TYPE GetBubbleType()
    {
        return BUBBLE_TYPE.IRON;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public override void Dead()
    {
        base.Dead();
    }

    public override void Reset()
    {
        bubbleHealth = 5;
        scoreWhenPopped = 10;
        moraleWhenPopped = 1;
        populationWhenPopped = 0;
    }
}
