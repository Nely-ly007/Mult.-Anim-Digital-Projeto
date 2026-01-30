using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDVictoryTextController : MonoBehaviour
{
    private TMP_Text _myText;

    private void OnEnable()
    {
        HUDObserverManager.ONPlayerVictory += OnPlayerVictory;
    }

    private void OnDisable()
    {
        HUDObserverManager.ONPlayerVictory -= OnPlayerVictory;
    }

    private void Awake()
    {
        _myText = GetComponent<TMP_Text>();
    }

    private void OnPlayerVictory(bool obj)
    {
        _myText.color = obj ? Color.white : Color.clear;
    }
}
