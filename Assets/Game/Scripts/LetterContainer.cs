using System;
using TMPro;
using UnityEngine;

public class LetterContainer : MonoBehaviour
{
    [Header("***Elements***")]
    [SerializeField] private TextMeshPro letter;
    public char Letter => this.letter.text[0];
    
    public void Initialize()
    {
        letter.text = "";        
    }

    public void SetLetter(char letter)
    {
        this.letter.text = letter.ToString();
    }

}
