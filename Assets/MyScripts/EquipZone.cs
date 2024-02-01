using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipZone : MonoBehaviour
{


    public event EventHandler<OnEquipToolEvetnArgs> OnEquipTool;


    

    public class OnEquipToolEvetnArgs : EventArgs
    {
        public float progressNormalized;
    }
    
    private float unEquipTimer;
    private float unEquipTimerMax = 2f;
    [SerializeField] private FarmingTool farmingTool;
    [SerializeField] private Transform resetPoint;
    
    


    

    private void OnTriggerStay(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        Debug.Log(farmingTool != null);
        Debug.Log(player!= null);


        if ((farmingTool != null) && player != null)
        {
            Debug.Log(unEquipTimer);

            unEquipTimer += Time.deltaTime;
            OnEquipTool?.Invoke(this, new OnEquipToolEvetnArgs
            {
                progressNormalized = unEquipTimer / unEquipTimerMax
            });
            if (unEquipTimer > unEquipTimerMax)
            {

                farmingTool.PlayerUnuseTool(resetPoint);
                unEquipTimer = 0f;
                Player.Instance.state = Player.State.Idle;
                OnEquipTool?.Invoke(this, new OnEquipToolEvetnArgs
                {
                    progressNormalized = 0f
                }) ;

            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FarmingTool farmingTool))
        {
            Debug.Log("out");
            OnEquipTool?.Invoke(this, new OnEquipToolEvetnArgs
            {
                progressNormalized = 0
            });
            unEquipTimer = 0;
        }
    }
}
