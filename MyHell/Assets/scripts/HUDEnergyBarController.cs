using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDEnergyBarController : MonoBehaviour
{
    private Slider _mySlider;

    private void OnEnable()
    {
        HUDObserverManager.ONPlayerEnergyChangedChannel += OnPlayerEnergyChangedChannel;
    }

    private void OnDisable()
    {
        HUDObserverManager.ONPlayerEnergyChangedChannel -= OnPlayerEnergyChangedChannel;
    }

    private void Awake()
    {
        _mySlider = GetComponent<Slider>();
    }

    private void OnPlayerEnergyChangedChannel(float obj)
    {
        _mySlider.value = obj;
    }
}
