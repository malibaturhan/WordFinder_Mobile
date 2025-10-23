using System;
using TMPro;
using UnityEngine;

public class LetterContainer : MonoBehaviour
{
    [Header("***Elements***")]
    [SerializeField] private TextMeshPro letter;
    [SerializeField] private SpriteRenderer letterContainer;
    public char Letter => this.letter.text[0];

    public void Initialize()
    {
        letter.text = "";
        letterContainer.color = Color.white;
    }

    public void SetLetter(char letter)
    {
        this.letter.text = letter.ToString();
    }

    public void SetValid()
    {
        letterContainer.color = Color.green;
    }

    public void SetPotential()
    {
        letterContainer.color = Color.yellow;
    }

    public void SetInvalid()
    {
        letterContainer.color = Color.gray4;
    }

}
