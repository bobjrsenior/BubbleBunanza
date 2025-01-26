using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public abstract class BubbleMovement : MonoBehaviour, IPointerDownHandler
{
    public new Rigidbody2D rigidbody2D;
    public float timeToFollow = 0.0f;
    public float followSpeed = 0.8f;
    public Vector2 followDirection = Vector2.zero;
    public Vector2 verticalDirection = Vector2.zero;
    public float verticalVelocity = 0.8f;
    public Vector2 overrideVector = Vector2.zero;

    protected AudioSource audioSource;
    protected SpriteRenderer spriteRenderer;
    public AudioClip pokeAudioClip;
    public AudioClip popAudioClip;

    protected int bubbleHealth = 1;

    protected int scoreWhenPopped = 1;
    protected int moraleWhenPopped = 0;
    protected int populationWhenPopped = 0;

    public enum BUBBLE_TYPE {
        NORMAL,
        DIRTY,
        IRON,
        PERSON,
        DIRTY_PERSON,
        SPIKE,
        TRENCHCOAT,
        TELEPORT,
        BOMB,
        TITLE
    }

    public BUBBLE_TYPE bubbleType;

    public DayHandler bubbleHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Awake ()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bubbleType = GetBubbleType();
    }

    protected abstract BUBBLE_TYPE GetBubbleType();

    // Update is called once per frame
    public void Move()
    {
        if (timeToFollow <= 0.0f)
        {
            followDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * followSpeed * Random.Range(0.3f, 2.5f);
            verticalDirection = Vector2.Perpendicular(followDirection.normalized);    
            timeToFollow = Random.Range(0.5f, 8.0f);
        }
        rigidbody2D.linearVelocity = followDirection + (verticalDirection * verticalVelocity * Mathf.Sin(Mathf.PI * Time.time));
        timeToFollow -= Time.deltaTime;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Poke();
    }

    public virtual void Poke()
    {
        bubbleHealth -= 1;
        if(pokeAudioClip != null)
        {
            GlobalVariables.instance.PlayAudioClip(pokeAudioClip);
        }
        if (bubbleHealth <= 0)
        {
            Dead();
        }
    }

    public virtual void Dead()
    {
        GlobalVariables.instance.PlayAudioClip(popAudioClip);
        GlobalVariables.instance.score += scoreWhenPopped;
        GlobalVariables.instance.population += populationWhenPopped;
        GlobalVariables.instance.morale += moraleWhenPopped;
        bubbleHandler.KillBubble(this);
    }

    public virtual void Reset()
    {
        bubbleHealth = 1;
        scoreWhenPopped = 1;
        moraleWhenPopped = 0;
        populationWhenPopped = 0;
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {//11
        // If we collided with a dirty bubble cloud
        if(collision.gameObject.layer == 10)
        {
            GameObject bubble;
            switch(bubbleType)
            {
                // Only regular and spike bubbles are converted
                case BUBBLE_TYPE.NORMAL:
                case BUBBLE_TYPE.SPIKE:
                    bubble = bubbleHandler.CreateBubble(BUBBLE_TYPE.DIRTY);
                    bubble.transform.position = transform.position;
                    bubble.SetActive(true);
                    bubbleHandler.KillBubble(this, false);
                    break;
                case BUBBLE_TYPE.PERSON:
                    bubble = bubbleHandler.CreateBubble(BUBBLE_TYPE.DIRTY_PERSON);
                    bubble.transform.position = transform.position;
                    bubble.SetActive(true);
                    bubbleHandler.KillBubble(this, false);
                    break;
            }
        }
        else if(collision.gameObject.layer == 11)
        {
            // If we collided with a bomb bubble cloud
            switch(bubbleType)
            {
                // Only regular and spike bubbles are converted
                case BUBBLE_TYPE.BOMB:
                    ((BombBubble)this).Explode();
                break;
                default:
                    var bubble = bubbleHandler.CreateBubble(BUBBLE_TYPE.TELEPORT);
                    bubble.transform.position = transform.position;
                    bubble.SetActive(true);
                    bubbleHandler.KillBubble(this, false);
                    break;
            }
        }
    }
}
