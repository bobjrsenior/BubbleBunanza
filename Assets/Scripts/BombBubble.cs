using UnityEngine;

public class BombBubble : BubbleMovement
{
    public AudioClip explodeAudioClip;
    public GameObject bombBubbleCloudPrefab;
    public GameObject markAlonePrefab;
    private float timeTilExplosion;

    void Start()
    {
        Reset();
    }

    protected override BUBBLE_TYPE GetBubbleType()
    {
        return BUBBLE_TYPE.BOMB;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        timeTilExplosion -= Time.deltaTime;
        if(timeTilExplosion <= 0)
        {
            Explode();
        }
    }

    public void Explode()
    {
        GlobalVariables.instance.PlayAudioClip(explodeAudioClip);
        Instantiate(bombBubbleCloudPrefab, this.transform.position, Quaternion.identity);
        scoreWhenPopped = -10;
        moraleWhenPopped = -10;
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        Instantiate(markAlonePrefab, this.transform.position, Quaternion.identity);
        //populationWhenPopped = -20;
        Dead();
    }

    public override void Dead()
    {
        base.Dead();
    }

    public override void Reset()
    {
        bubbleHealth = 25;
        scoreWhenPopped = 20;
        moraleWhenPopped = 5;
        populationWhenPopped = 0;
        timeTilExplosion = 10.0f - (GlobalVariables.instance.day * 0.25f);
    }
}
