using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class FillUpZoneVisuals : MonoBehaviour
{
    [SerializeField] private FillUpZone fillUpZone;
    [SerializeField] private Image barImage;


    private void Start()
    {
        fillUpZone.OnFill += FillUpZone_OnFillPlants;
    }

    private void FillUpZone_OnFillPlants(object sender, FillUpZone.OnEquipToolEvetnArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        if(e.progressNormalized == 0 || e.progressNormalized == 1)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }



    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}
