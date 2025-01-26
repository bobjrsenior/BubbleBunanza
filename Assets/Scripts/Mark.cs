using UnityEngine;

public class Mark : MonoBehaviour
{
    public Sprite markDeadSprite;
    public bool dead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-450, 450f) + 75f;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!dead)
        {
            dead = true;
            GlobalVariables.instance.population -= 1;
            GlobalVariables.instance.morale -= 1;
            GetComponent<SpriteRenderer>().sprite = markDeadSprite;
        }
    }
}
