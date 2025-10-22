using System;
using UnityEngine;

public class WordContainer : MonoBehaviour
{
    [Header("***Elements***")]
    private LetterContainer[] letterContainers;
    void Awake()
    {
        letterContainers = GetComponentsInChildren<LetterContainer>();
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < letterContainers.Length; i++) 
        {
            letterContainers[i].Initialize();
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
