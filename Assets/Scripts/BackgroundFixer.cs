using UnityEngine;

public class BackgroundFixer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
