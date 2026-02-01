using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNameTag : W2C
{
    [SerializeField] TMP_Text _text;

    public void Init(Transform player, string text)
    {
        _text.text = text;
        SetPosition(player);
    }
}
