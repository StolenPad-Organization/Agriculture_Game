using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaterToolVisual : MonoBehaviour
{
    [SerializeField] private Watering watering;
    [SerializeField] private TextMeshProUGUI waterEmptyText;

    private float min = 1;
    private float max = 1.6f;
    private Vector3 initialScale;

    private void Start()
    {
        initialScale = waterEmptyText.transform.localScale;

        watering.OnWaterEmptyIn += Watering_OnWaterEmptyIn;
        watering.OnWaterEmptyOut += Watering_OnWaterEmptyOut;
    }

    private void Watering_OnWaterEmptyOut(object sender, System.EventArgs e)
    {
        waterEmptyText.gameObject.SetActive(false);
    }

    private void Watering_OnWaterEmptyIn(object sender, System.EventArgs e)
    {
        waterEmptyText.gameObject.SetActive(true);

        LeanTween.scale(waterEmptyText.gameObject, initialScale * max, min)
            .setEase(LeanTweenType.easeInOutSine)
            .setLoopPingPong(-1);
    }
}
