using UnityEngine;

public class TeleportBubble : BubbleMovement
{
    protected override BUBBLE_TYPE GetBubbleType()
    {
        return BUBBLE_TYPE.TELEPORT;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public override void Reset()
    {
        bubbleHealth = 5;
        scoreWhenPopped = 12;
        moraleWhenPopped = 0;
        populationWhenPopped = 0;
    }

    public override void Poke()
    {
        if(bubbleHealth > 1)
        {
            transform.position = new Vector2(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f));
        }
        base.Poke();
    }
}
