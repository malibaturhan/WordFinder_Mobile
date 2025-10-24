using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

enum Validity
{
    None,
    Valid,
    Potential,
    Invalid
}

public class KeyboardKey : MonoBehaviour
{
    [Header("***Elements***")]
    [SerializeField] private TextMeshProUGUI letterText;
    [SerializeField] private Image renderer;

    [Header("---Settings---")]
    private Validity validity;

    [Header("---Events---")]
    public static Action<char> onKeyPressed;

    void Start()
    {
        Initialize();
        GetComponent<Button>().onClick.AddListener(SendKeyPressedEvent);
    }

    public bool IsUntouched => validity == Validity.None;

    public void Initialize()
    {
        renderer.color = Color.white;
        validity = Validity.None;
    }

    public char Letter => letterText.text[0];
        
    private void SendKeyPressedEvent()
    {
        
        onKeyPressed?.Invoke(letterText.text[0]);
    }

    public void SetValid()
    {
        renderer.color = Color.green;
        validity = Validity.Valid;
    }

    public void SetPotential()
    {
        if(validity == Validity.Valid) return;
        renderer.color = Color.yellow;
        validity = Validity.Potential;
    }

    public void SetInvalid()
    {
        if(validity == Validity.Valid || validity == Validity.Potential) return;
        renderer.color = Color.gray4;
        validity = Validity.Invalid;
        
    }
}
