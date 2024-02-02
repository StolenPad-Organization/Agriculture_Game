using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipZoneVisuals : MonoBehaviour
{
    [SerializeField] private EquipZone equipZone;
    [SerializeField] private Image barImage;


    private void Start()
    {
        equipZone.OnEquipTool += EquipZone_OnEquipTool;
    }

    private void EquipZone_OnEquipTool(object sender, EquipZone.OnEquipToolEvetnArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        
    }

    
}

