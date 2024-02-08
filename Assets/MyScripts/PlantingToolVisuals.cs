using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlantingToolVisuals : MonoBehaviour
{
    [SerializeField] private Planting planting;
    [SerializeField] private TextMeshProUGUI seedsEmptyText;


    private float min = 1;
    private float max = 1.6f;
    private Vector3 initialScale;


    private void Start()
    {
        initialScale = seedsEmptyText.transform.localScale;

        planting.OnSeedsEmptyIn += Planting_OnSeedsEmptyIn;
        planting.OnSeedsEmptyOut += Planting_OnSeedsEmptyOut;
    }

    private void Planting_OnSeedsEmptyOut(object sender, System.EventArgs e)
    {
        seedsEmptyText.gameObject.SetActive(false);

    }

    private void Planting_OnSeedsEmptyIn(object sender, System.EventArgs e)
    {
        seedsEmptyText.gameObject.SetActive(true);

        LeanTween.scale(seedsEmptyText.gameObject, initialScale * max, min)
            .setEase(LeanTweenType.easeInOutSine)
            .setLoopPingPong(-1);
    }
}




