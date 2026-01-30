using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDKeyController : MonoBehaviour
{
    [SerializeField] private Image keyIcon;
    
    private void OnEnable()
    {
        HUDObserverManager.ONKeyChanged += OnKeyGet;
    }

    private void OnDisable()
    {
        HUDObserverManager.ONKeyChanged -= OnKeyGet;
    }

    private void OnKeyGet(bool key)
    {
        keyIcon.color = key ?  Color.white: Color.clear;
    }
}
