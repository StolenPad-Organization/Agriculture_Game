using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Selling : MonoBehaviour
{
    public static Selling Instance { get; private set; }
    public event EventHandler OnReset;

    [SerializeField] private Transform playerCollectabeHoldPoint;
    [SerializeField] private Transform sellingPoint;
    [SerializeField] private Transform collectArea;
    [SerializeField] private GameObject collectablePrefab;
    [SerializeField] private float collectableSpeed;
    [SerializeField] private float durationBetweenCollectables;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private int chunksPerRow = 5;
    private int currentRow = 0;
    private int maxRows = 3;
    private float zOffset = 0.2f;
    private float currentZ;
    private float maxColumn = 2;
    private float timeToWaitFactor = 0.02f;
    private float timeToSetChunks = 0.0000000001f;



    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (playerCollectabeHoldPoint.childCount != 0)
            {
                StartCoroutine(SellCollectablesCouroutine());
                StartCoroutine(SpawnMoney(playerCollectabeHoldPoint.childCount));
                virtualCamera.gameObject.SetActive(true);

            }
        }
    }

    private IEnumerator SellCollectablesCouroutine()
    {
        while (playerCollectabeHoldPoint.childCount > 0)
        {
            List<Transform> objectsToSell = new List<Transform>();
            for (int i = 0; i < playerCollectabeHoldPoint.childCount; i++)
            {
                objectsToSell.Add(playerCollectabeHoldPoint.GetChild(i));
            }
            Vector3 targetPosition = sellingPoint.position;
            for (int i = objectsToSell.Count - 1; i >= 0; i--)
            {
                Transform objectToSell = objectsToSell[i];
                objectToSell.DOMove(targetPosition, collectableSpeed)
                             .SetEase(Ease.InOutQuad)
                             .SetSpeedBased()
                             .OnComplete(() =>
                             {
                                 Destroy(objectToSell.gameObject);
                             });

                yield return new WaitForSeconds(durationBetweenCollectables);
            }
            yield return null;
        }
    }
    //private IEnumerator SellCollectablesCouroutine(int chunks)
    //{
    //    while (playerCollectabeHoldPoint.childCount > 0)
    //    {
    //        Transform objectToSell = playerCollectabeHoldPoint.GetChild(playerCollectabeHoldPoint.childCount - 1);
    //        Vector3 targetPosition = sellingPoint.position;
    //        SpawnMoney(new Vector3(10, 10, 10));

    //        yield return new WaitForSeconds(durationBetweenCollectables);
    //        objectToSell.DOMove(targetPosition, collectableSpeed).SetSpeedBased()
    //            .SetEase(Ease.OutQuad)
    //            .OnComplete(() =>
    //            {
    //                Destroy(objectToSell.gameObject);
    //                OnReset?.Invoke(this, EventArgs.Empty);
    //            });
    //        yield return null;
    //    }
    //}

    private IEnumerator SpawnMoney(int chunksToSell)
    {
        float timeToWait = chunksToSell * timeToWaitFactor;
        yield return new WaitForSeconds(timeToWait);
        for (int i = 0; i < chunksToSell; i++)
        {
            GameObject newCollectablePrefab = Instantiate(collectablePrefab, sellingPoint.position, Quaternion.identity, collectArea);
            newCollectablePrefab.TryGetComponent(out BoxCollider collectablePrefabCollider);
            collectablePrefabCollider.enabled = false;
            OnReset?.Invoke(this, EventArgs.Empty);


            float prefabWidth = newCollectablePrefab.transform.localScale.x;
            float prefabHeight = newCollectablePrefab.transform.localScale.z;

            int column = i % chunksPerRow;

            if (column == 0 && i > 0)
            {
                currentRow++;
            }

            if (currentRow >= maxRows)
            {
                currentRow = 0;
                currentZ += zOffset;
            }
            if (currentZ >= maxColumn)
            {
                currentZ = 0;
            }

            Vector3 newPosition = collectArea.position + new Vector3(column * prefabWidth, currentRow * prefabHeight, currentZ);

            float delay = chunksToSell * timeToSetChunks;
            yield return new WaitForSeconds(delay);

            if (newCollectablePrefab != null)
            {
                newCollectablePrefab.transform.DOMove(newPosition, 0.6f).SetEase(Ease.OutQuad).OnComplete(() =>
                {
                    collectablePrefabCollider.enabled = true;
                });

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //StopCoroutine(SpawnMoney(playerCollectabeHoldPoint.childCount));
        //StopCoroutine(SellCollectablesCouroutine(playerCollectabeHoldPoint.childCount));
        StopAllCoroutines();
        virtualCamera.gameObject.SetActive(false);

    }
}
