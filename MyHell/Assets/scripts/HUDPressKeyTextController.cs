using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDPressKeyTextController : MonoBehaviour
{
    private TMP_Text _myText;

    private void OnEnable()
    {
        HUDObserverManager.ONPlayerVictory += OnPressKeyTextChange;
        HUDObserverManager.ONPlayerDeath += OnPressKeyTextChange;
    }

    private void OnDisable()
    {
        HUDObserverManager.ONPlayerVictory -= OnPressKeyTextChange;
        HUDObserverManager.ONPlayerDeath -= OnPressKeyTextChange;
    }

    private void Awake()
    {
        _myText = GetComponent<TMP_Text>();
    }

    private void OnPressKeyTextChange(bool obj)
    {
        _myText.color = obj ? Color.white : Color.clear;
    }
}
