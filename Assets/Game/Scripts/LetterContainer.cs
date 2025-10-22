using System;
using TMPro;
using UnityEngine;

public class LetterContainer : MonoBehaviour
{
    [Header("***Elements***")]
    [SerializeField] private TextMeshPro letter;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Initialize()
    {
        letter.text = "";        
    }
}
