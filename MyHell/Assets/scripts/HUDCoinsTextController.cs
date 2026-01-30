using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDCoinsTextController : MonoBehaviour
{

    [SerializeField] private Image playerIcon;
    
    private TMP_Text _text;
    
    private void OnEnable()
    {
        HUDObserverManager.ONCoinsChanged += OnCoinsChangedChannel;
    }

    private void OnDisable()
    {
        HUDObserverManager.ONCoinsChanged -= OnCoinsChangedChannel;
    }

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnCoinsChangedChannel(int obj)
    {
        _text.text = obj.ToString();
        _text.color = obj < 0 ? Color.clear : Color.white;
        playerIcon.color = obj < 0 ? Color.clear : Color.white;
    }
}