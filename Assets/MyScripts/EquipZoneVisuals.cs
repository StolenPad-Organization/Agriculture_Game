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
        Debug.Log(e.progressNormalized);
        if (e.progressNormalized == 0 || e.progressNormalized == 1)
        {
            Hide();
            Debug.Log("hide");
        }
        else
        {
            Debug.Log("show");
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

