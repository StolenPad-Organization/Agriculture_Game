using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Planting : MonoBehaviour
{


    public event EventHandler OnSeedsEmptyIn;
    public event EventHandler OnSeedsEmptyOut;
    [SerializeField] Transform seedInitialPosition;
    [SerializeField] Transform seedPrefab;
    private FarmingAreaSatus farmingAreaSatus;


    public int plantsNumber = 0;


    private bool isPlanting;
    private float maxSeedScacle = 0.16f;
    private float minsSeedScacle = 0.1f;




    private void Update()
    {
        if (farmingAreaSatus != null)
        {
            if (farmingAreaSatus.IsLoosingCompleted())
            {
                isPlanting = true;
            }
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (isPlanting && collision.gameObject.CompareTag(Constants.DIRT))
        {
            MeshRenderer meshRenderer = collision.gameObject.GetComponent<MeshRenderer>();


            if (meshRenderer != null && meshRenderer.enabled && plantsNumber != 0 && !collision.transform.GetChild(0).gameObject.activeSelf && !collision.transform.GetChild(1).gameObject.activeSelf)
            {
                plantsNumber--;
                Transform seedTransform = collision.transform.GetChild(0);
                Transform seedPrefabTransform = Instantiate(seedPrefab, seedInitialPosition.position, Quaternion.identity);
                seedPrefabTransform.DOMove(seedTransform.position, 1.5f).OnComplete(() =>
                {
                    seedTransform.gameObject.SetActive(true);
                    Destroy(seedPrefabTransform.gameObject); // Destroy the seed's GameObject
                    seedTransform.localScale = Vector3.one * minsSeedScacle;
                    seedTransform.DOScale(Vector3.one * maxSeedScacle, 1f)
                                    .SetEase(Ease.OutBounce);
                });
                Debug.Log("seed");

            }
            if (plantsNumber == 0)
            {
                OnSeedsEmptyIn?.Invoke(this, EventArgs.Empty);
            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FarmingAreaSatus farmingAreaSatus))
        {
            this.farmingAreaSatus = farmingAreaSatus;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FarmingAreaSatus farmingAreaSatus))
        {
            OnSeedsEmptyOut.Invoke(this, EventArgs.Empty);
        }
    }


    public int GetPlantsNumber()
    {
        return plantsNumber;
    }

}
