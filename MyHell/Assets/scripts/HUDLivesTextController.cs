using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDLivesTextController : MonoBehaviour
{

    [SerializeField] private Image playerIcon;
    
    private TMP_Text _text;
    
    private void OnEnable()
    {
        HUDObserverManager.ONLivesChangedChannel += OnLivesChangedChannel;
    }

    private void OnDisable()
    {
        HUDObserverManager.ONLivesChangedChannel -= OnLivesChangedChannel;
    }

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnLivesChangedChannel(int obj)
    {
        _text.text = "x"+obj.ToString();
        _text.color = obj < 0 ? Color.clear : Color.white;
        playerIcon.color = obj < 0 ? Color.clear : Color.white;
    }
}
