using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalVariables : MonoBehaviour
{
    public static GlobalVariables instance;

    public int score = 0;
    public int morale = 100;
    public int population = 50;
    public int day = 0;

    public int bubblesPopped = 0;

    public bool gameOver = false;

    public TMP_Text tmpUI;

    public AudioSource[] audioSources;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            var obj = GameObject.FindGameObjectWithTag("ScoreText");
            if (obj != null)
            {
                tmpUI = obj.GetComponent<TMP_Text>();
            }
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var obj = GameObject.FindGameObjectWithTag("ScoreText");
        if (obj != null)
        {
            tmpUI = obj.GetComponent<TMP_Text>();
        }

        // Stop looping audio from continue to loop in the new scene
        foreach(AudioSource audioSource in audioSources)
        {
            audioSource.loop = false;
            audioSource.volume = 0.05f;
        }
    }

    public void ResetData()
    {
        score = 0;
        morale = 100;
        population = 50;
        day = 1;
        gameOver = false;
    }

    public void PlayAudioClip(AudioClip audioClip, bool loop = false)
    {
        foreach(AudioSource audioSource in audioSources)
        {
            if(!audioSource.isPlaying)
            {
                audioSource.clip = audioClip;
                if (loop)
                {
                    audioSource.loop = loop;
                }
                audioSource.volume = 1.0f;
                audioSource.Play();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tmpUI != null)
        {
            tmpUI.text = "Day: " + day + "\nScore: " + score + "\nPopulation: " + population + "\nMorale: " + morale;
        }
    }
}
