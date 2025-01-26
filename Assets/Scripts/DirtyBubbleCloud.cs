using UnityEngine;

public class DirtyBubbleCloud : MonoBehaviour
{
    public float timeToLive = 1.0f;
    public float scalePerSecond = 2.25f;

    // Update is called once per frame
    void Update()
    {
        timeToLive -= Time.deltaTime;
        if(timeToLive < 0.0f)
        {
            Destroy(this.gameObject);
        }
        transform.localScale += new Vector3(1.0f, 1.0f, 0.0f) * scalePerSecond * Time.deltaTime;
    }
}
