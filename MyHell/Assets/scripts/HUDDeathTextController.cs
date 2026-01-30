using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDDeathTextController : MonoBehaviour
{
    private TMP_Text _myText;

    private void OnEnable()
    {
        HUDObserverManager.ONPlayerDeath += OnPlayerDeath;
    }

    private void OnDisable()
    {
        HUDObserverManager.ONPlayerDeath -= OnPlayerDeath;
    }

    private void Awake()
    {
        _myText = GetComponent<TMP_Text>();
    }

    private void OnPlayerDeath(bool obj)
    {
        _myText.color = obj ? Color.white : Color.clear;
    }
}
