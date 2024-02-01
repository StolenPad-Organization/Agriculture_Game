using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selling : MonoBehaviour
{

    public static Selling Instance { get; private set; }

    

    public event EventHandler OnReset;

    

    [SerializeField] private Transform playerCollectabeHoldPoint;
    [SerializeField] private FarmedObjectSO farmedObjectSO; 

    private float sellingTimer;
    private float sellingTimerMax = 0.05f;
    private float waitForPlayerTimer;
    private float waitForPlayerTimerMax = 2f;





    private void Awake()
    {
        Instance = this;
    }
    private void OnTriggerStay(Collider other)
    {
        waitForPlayerTimer += Time.deltaTime;
        if (other.gameObject.TryGetComponent(out Player player) && waitForPlayerTimer > waitForPlayerTimerMax)
        {
            if (playerCollectabeHoldPoint.childCount != 0)
            {
                // the player has collectable
                SellCollectables();
            }
        }
        

    }


    private void SellCollectables()
    {
        sellingTimer += Time.deltaTime;

        if (sellingTimer > sellingTimerMax)
        {
            sellingTimer = 0f;

            if (playerCollectabeHoldPoint.childCount > 0)
            {
                Destroy(playerCollectabeHoldPoint.GetChild(playerCollectabeHoldPoint.childCount - 1).gameObject);
                OnReset?.Invoke(this,EventArgs.Empty);
            }
        }
    }
}
