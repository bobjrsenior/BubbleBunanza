using UnityEngine;

public class PersonBubble : BubbleMovement
{
    public GameObject markAlonePrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Reset();
    }

    protected override BUBBLE_TYPE GetBubbleType()
    {
        return BUBBLE_TYPE.PERSON;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public override void Dead()
    {
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        base.Dead();
    }

    public override void Reset()
    {
        bubbleHealth = 1;
        scoreWhenPopped = 1;
        moraleWhenPopped = 1;
        populationWhenPopped = 0;
    }
}
