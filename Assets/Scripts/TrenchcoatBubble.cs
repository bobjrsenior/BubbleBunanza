using UnityEngine;

public class TrenchcoatBubble : BubbleMovement
{
    public bool trenchcoatIsOpen = false;
    public AudioClip openPoke;
    public AudioClip openSound;
    public AudioClip closedPoke;
    public Sprite closedSprite;
    public Sprite openSprite;
    public float timeToOpen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Reset();
        timeToOpen = Random.Range(2.0f, 5.0f);
    }

    protected override BUBBLE_TYPE GetBubbleType()
    {
        return BUBBLE_TYPE.TRENCHCOAT;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        timeToOpen -= Time.deltaTime;
        if (timeToOpen <= 0)
        {
            if (trenchcoatIsOpen)
            {
                spriteRenderer.sprite = closedSprite;
                pokeAudioClip = closedPoke;
            }
            else
            {
                GlobalVariables.instance.PlayAudioClip(openSound);
                spriteRenderer.sprite = openSprite;
                pokeAudioClip = openPoke;
            }
            trenchcoatIsOpen = !trenchcoatIsOpen;
            timeToOpen = Random.Range(2.0f, 5.0f);
        }
    }

    public override void Poke()
    {
        if(trenchcoatIsOpen)
        {
            base.Poke();
        }
    }

    public override void Dead()
    {
        base.Dead();
    }

    public override void Reset()
    {
        bubbleHealth = 4;
        scoreWhenPopped = 12;
        moraleWhenPopped = 2;
        populationWhenPopped = 0;
    }
}
