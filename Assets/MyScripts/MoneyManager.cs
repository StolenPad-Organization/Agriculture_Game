using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }


    [SerializeField] private TextMeshProUGUI moneyText;


    private int moneyAmount;



    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        moneyText.text = moneyAmount.ToString() + "$";
    }


    public void AddMoney()
    {
        moneyAmount++;
        moneyText.text = moneyAmount.ToString() + "$";
    }
}
