using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayHandler : MonoBehaviour
{
    public Stack<GameObject> bubblesForTheDay;
    private Dictionary<BubbleMovement.BUBBLE_TYPE, Stack<GameObject>> objectPool;

    public GameObject normalBubblePrefab;
    public GameObject dirtyBubblePrefab;
    public GameObject ironBubblePrefab;
    public GameObject personBubblePrefab;
    public GameObject dirtyPersonBubblePrefab;
    public GameObject spikeBubblePrefab;
    public GameObject trenchcoatBubblePrefab;
    public GameObject titleBubblePrefab;
    public GameObject teleportBubblePrefab;
    public GameObject bombBubblePrefab;
    public Vector2 poolLocation = new Vector2(1000.0f, 1000.0f);

    public bool dayStarted = false;
    public float spawnTimer;
    public float spawnCountdown;
    public int poppedToday = 0;
    public float delayBetweenDays = 1.5f;
    public float dayDelayCountdown;
    public int numBubblesForToday;
    public bool autoSpawnEnabled = false;
    private int[] spawnRates = {25, 0, 5, 5, 5, 5, 5, 4, 2};
    private BubbleMovement.BUBBLE_TYPE[] spawnTypes = 
    {
        BubbleMovement.BUBBLE_TYPE.NORMAL,
        BubbleMovement.BUBBLE_TYPE.NORMAL,
        BubbleMovement.BUBBLE_TYPE.DIRTY,
        BubbleMovement.BUBBLE_TYPE.PERSON,
        BubbleMovement.BUBBLE_TYPE.IRON,
        BubbleMovement.BUBBLE_TYPE.TRENCHCOAT,
        BubbleMovement.BUBBLE_TYPE.SPIKE,
        BubbleMovement.BUBBLE_TYPE.TELEPORT,
        BubbleMovement.BUBBLE_TYPE.BOMB,
    };

    public AudioClip[] dayStartAudioClips;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        bubblesForTheDay = new Stack<GameObject>();
        objectPool = new Dictionary<BubbleMovement.BUBBLE_TYPE, Stack<GameObject>>();
        objectPool.Add(BubbleMovement.BUBBLE_TYPE.NORMAL, new Stack<GameObject>());
        objectPool.Add(BubbleMovement.BUBBLE_TYPE.DIRTY, new Stack<GameObject>());
        objectPool.Add(BubbleMovement.BUBBLE_TYPE.PERSON, new Stack<GameObject>());
        objectPool.Add(BubbleMovement.BUBBLE_TYPE.DIRTY_PERSON, new Stack<GameObject>());
        objectPool.Add(BubbleMovement.BUBBLE_TYPE.IRON, new Stack<GameObject>());
        objectPool.Add(BubbleMovement.BUBBLE_TYPE.TRENCHCOAT, new Stack<GameObject>());
        objectPool.Add(BubbleMovement.BUBBLE_TYPE.SPIKE, new Stack<GameObject>());
        objectPool.Add(BubbleMovement.BUBBLE_TYPE.TELEPORT, new Stack<GameObject>());
        objectPool.Add(BubbleMovement.BUBBLE_TYPE.BOMB, new Stack<GameObject>());
        objectPool.Add(BubbleMovement.BUBBLE_TYPE.TITLE, new Stack<GameObject>());
    }

    void Start()
    {
        if (GlobalVariables.instance.day > 0 && !GlobalVariables.instance.gameOver)
        {
            autoSpawnEnabled = true;
        }
        StartDay();
    }

    public void StartDay()
    {
        int day = GlobalVariables.instance.day;
        if (autoSpawnEnabled)
        {
            numBubblesForToday = day * 10;
            spawnTimer = 0.8f - (0.025f * day);
            if(spawnTimer < 0.01f)
                spawnTimer = 0.01f;
            spawnCountdown = spawnTimer;

            ClearOutDayBubbles();

            //Generate bubble list
            BubbleMovement.BUBBLE_TYPE[] bubbleTypes = new BubbleMovement.BUBBLE_TYPE[numBubblesForToday];
            for(int i = 0; i < numBubblesForToday; i++)
            {
                bubbleTypes[i] = DetermineBubbleType(day);
            }

            // Make sure we have at least 1 level specific bubble
            BubbleMovement.BUBBLE_TYPE requiredBubbleType = BubbleMovement.BUBBLE_TYPE.NORMAL;
            if(day == 3)
                requiredBubbleType = BubbleMovement.BUBBLE_TYPE.DIRTY;
            else if(day == 4)
                requiredBubbleType = BubbleMovement.BUBBLE_TYPE.PERSON;
            else if(day == 5)
                requiredBubbleType = BubbleMovement.BUBBLE_TYPE.IRON;
            else if(day == 6)
                requiredBubbleType = BubbleMovement.BUBBLE_TYPE.TRENCHCOAT;
            else if(day == 7)
                requiredBubbleType = BubbleMovement.BUBBLE_TYPE.SPIKE;
            else if(day == 8)
                requiredBubbleType = BubbleMovement.BUBBLE_TYPE.TELEPORT;
            else if(day == 9)
                requiredBubbleType = BubbleMovement.BUBBLE_TYPE.BOMB;
            
            // Replace the last bubble with the required one
            // It's a stack so it will actually spawn first
            if (requiredBubbleType != BubbleMovement.BUBBLE_TYPE.NORMAL)
            {
                bubbleTypes[bubbleTypes.Length - 1] = requiredBubbleType;
            }

            // Finally create the bubbles
            for(int i = 0; i < numBubblesForToday; i++)
            {
                bubblesForTheDay.Push(CreateBubble(bubbleTypes[i]));
            }
            poppedToday = 0;
            dayStarted = true;
            if(day <= dayStartAudioClips.Length)
            {
                GlobalVariables.instance.PlayAudioClip(dayStartAudioClips[day - 1]);
            }
        }
    }

    public BubbleMovement.BUBBLE_TYPE DetermineBubbleType(int day)
    {
        var totalValue = 0;
        for(int i = 0;i < day && i < spawnRates.Length; i++)
        {
            totalValue += spawnRates[i];
        }
        var randVal = Random.Range(0, totalValue);
        for(int i = 0;i < day && i < spawnRates.Length; i++)
        {
            randVal -= spawnRates[i];
            if(randVal <= 0)
            {
                return spawnTypes[i];
            }
        }

        return BubbleMovement.BUBBLE_TYPE.NORMAL;
    }

    public void ClearOutDayBubbles()
    {
        foreach(GameObject obj in bubblesForTheDay)
        {
            KillBubble(obj.GetComponent<BubbleMovement>());
        }
        bubblesForTheDay.Clear();
    }

    public GameObject CreateBubble(BubbleMovement.BUBBLE_TYPE type)
    {
        GameObject bubble = null;
        GameObject prefab = null;
        switch(type)
        {
            case BubbleMovement.BUBBLE_TYPE.NORMAL:
                prefab = normalBubblePrefab;
            break;
            case BubbleMovement.BUBBLE_TYPE.DIRTY:
                prefab = dirtyBubblePrefab;
            break;
            case BubbleMovement.BUBBLE_TYPE.PERSON:
                prefab = personBubblePrefab;
            break;
            case BubbleMovement.BUBBLE_TYPE.DIRTY_PERSON:
                prefab = dirtyPersonBubblePrefab;
            break;
            case BubbleMovement.BUBBLE_TYPE.IRON:
                prefab = ironBubblePrefab;
            break;
            case BubbleMovement.BUBBLE_TYPE.TRENCHCOAT:
                prefab = trenchcoatBubblePrefab;
            break;
            case BubbleMovement.BUBBLE_TYPE.SPIKE:
                prefab = spikeBubblePrefab;
            break;
            case BubbleMovement.BUBBLE_TYPE.TELEPORT:
                prefab = teleportBubblePrefab;
            break;
            case BubbleMovement.BUBBLE_TYPE.BOMB:
                prefab = bombBubblePrefab;
            break;
            case BubbleMovement.BUBBLE_TYPE.TITLE:
                prefab = titleBubblePrefab;
            break;
        }
        if(!objectPool[type].TryPop(out bubble))
        {
            bubble = Instantiate(prefab, poolLocation, Quaternion.identity) as GameObject;
            bubble.SetActive(false);
        }
        var bubbleScript = bubble.GetComponent<BubbleMovement>();
        bubbleScript.Reset();
        bubbleScript.bubbleHandler = this;
        return bubble;
    }

    public void SpawnBubble(GameObject bubble)
    {
        bubble.transform.position = new Vector2(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f));
        bubble.SetActive(true);
    }

    public void KillBubble(BubbleMovement bubble, bool countKill = true)
    {
        bubble.Reset();
        bubble.transform.position = poolLocation;
        bubble.gameObject.SetActive(false);
        objectPool[bubble.bubbleType].Push(bubble.gameObject);
        if (dayStarted && countKill)
        {
            GlobalVariables.instance.bubblesPopped += 1;
            poppedToday += 1;
            if (poppedToday >= numBubblesForToday)
            {
                dayDelayCountdown = delayBetweenDays;
                dayStarted = false;
                GlobalVariables.instance.day += 1;
            }
            if(GlobalVariables.instance.morale <= 0 || GlobalVariables.instance.population <= 0)
            {
                GlobalVariables.instance.gameOver = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dayStarted)
        {
            spawnCountdown -= Time.deltaTime;
            if(spawnCountdown < 0)
            {
                spawnCountdown = spawnTimer;
                GameObject bubbleToSpawn;
                if(bubblesForTheDay.TryPop(out bubbleToSpawn))
                {
                    SpawnBubble(bubbleToSpawn);
                }
                else{
                    // End Day if no bubbles are left outstanding
                }
            }
        }
        else if(autoSpawnEnabled)
        {
            dayDelayCountdown -= Time.deltaTime;
            if(dayDelayCountdown <= 0)
            {
                StartDay();
            }
        }
    }
}
