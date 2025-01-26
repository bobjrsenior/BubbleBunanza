using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class NormalBubble : BubbleMovement
{
    protected override BUBBLE_TYPE GetBubbleType()
    {
        return BUBBLE_TYPE.NORMAL;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public override void Dead()
    {
        if (GlobalVariables.instance.morale < 100)
        {
            moraleWhenPopped += 1;
        }
        if(GlobalVariables.instance.bubblesPopped % 10 == 0)
        {
            populationWhenPopped += 1;
        }
        base.Dead();
    }
}
