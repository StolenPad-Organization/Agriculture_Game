using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    [SerializeField] private Transform ToolresetPoint;
    [SerializeField] private GameObject LoadVisuals;








    private void OnTriggerStay(Collider other)
    {
        if ((farmingTool != null) && other.TryGetComponent(out Player player) && (Player.Instance.PlayerHasPushingTool() || Player.Instance.PlayerHasHandTool()) && !ZoneIsFull(ToolresetPoint))
        {

            unEquipTimer += Time.deltaTime;
            OnEquipTool?.Invoke(this, new OnEquipToolEvetnArgs
            {
                progressNormalized = unEquipTimer / unEquipTimerMax
            });
            if (unEquipTimer > unEquipTimerMax)
            {

                farmingTool.PlayerUnuseTool(ToolresetPoint);
                unEquipTimer = 0f;
                Player.Instance.state = Player.State.Idle;
                OnEquipTool?.Invoke(this, new OnEquipToolEvetnArgs
                {
                    progressNormalized = 0f
                });
                LoadVisuals.SetActive(false);

            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FarmingTool farmingTool))
        {
            OnEquipTool?.Invoke(this, new OnEquipToolEvetnArgs
            {
                progressNormalized = 0
            });
            unEquipTimer = 0;
            LoadVisuals.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FarmingTool farmingTool) && (Player.Instance.PlayerHasPushingTool() || Player.Instance.PlayerHasHandTool()))
        {
            LoadVisuals.SetActive(true);
        }
    }

    private bool ZoneIsFull(Transform HoldArea)
    {
        return HoldArea.childCount != 0;
    }

}
