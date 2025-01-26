using UnityEngine;

public class LittleBubble : MonoBehaviour
{
    //private Vector2 velocity;
    private float timeToLive = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //GetComponent<Rigidbody2D>().linearVelocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive < 0)
        {
            Destroy(this.gameObject);
        }
    }

        void OnCollisionEnter2D(Collision2D col)
    {
        var obj = col.gameObject.GetComponent<BubbleMovement>();
        if(obj != null)
        {
            obj.Poke();
            Destroy(this.gameObject);
        }
    }
}
