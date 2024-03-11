using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickCoinsUpdate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clickCoinsTxt;

    private void OnEnable()
    {
        ClickerManager.OnValueChanged += UpdateTxt;
    }

    private void OnDisable()
    {
        ClickerManager.OnValueChanged += UpdateTxt;        
    }

    void UpdateTxt(int value)
    {
        clickCoinsTxt.text = value.ToString();
    }
}
