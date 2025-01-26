using UnityEngine;

public class DirtyBubble : BubbleMovement
{
    public GameObject dirtyBubbleCloudPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Reset();
    }

    protected override BUBBLE_TYPE GetBubbleType()
    {
        return BUBBLE_TYPE.DIRTY;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public override void Dead()
    {
        Instantiate(dirtyBubbleCloudPrefab, this.transform.position, Quaternion.identity);
        base.Dead();
    }

    public override void Reset()
    {
        bubbleHealth = 1;
        scoreWhenPopped = 5;
        moraleWhenPopped = -3;
        populationWhenPopped = 0;
    }
}
