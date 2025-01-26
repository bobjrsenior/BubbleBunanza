using UnityEngine;

public class PlayThemeSong : MonoBehaviour
{
    public AudioClip themeSong;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalVariables.instance.PlayAudioClip(themeSong, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
