using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningAndLoosing : MonoBehaviour
{
    private enum State
    {
        cleaning,
        loosing,
        harvesting

    }

    private State state;

    private FarmingAreaSatus farmingAreaSatus;
    private Transform collectablesHoldPoint;
    [SerializeField] private Transform readyToPlantPrefab;

    private List<Transform> collectabledChunksList = new List<Transform>();
    private int maxChunks = 50;
    private int chunkCount = 0;
    private float grassOffsetAmount = 0.213f;
    private float tomatoOffsetAmount = 0.2f;







    private void Start()
    {
        collectablesHoldPoint = Player.Instance.GetCollectablesHoldPoint();
        Selling.Instance.OnReset += Instance_OnReset;
        state = State.cleaning;
    }


    private void Update()
    {
        if (farmingAreaSatus != null && state != State.harvesting)
        {
            if (farmingAreaSatus.IsCleaning())
            {
                state = State.cleaning;
            }
            if (farmingAreaSatus.IsCleaningCompleted())
            {
                state = State.loosing;
            }
            if (farmingAreaSatus.IsWateringComplete())
            {
                state = State.harvesting;
            }

        }

    }

    private void Instance_OnReset(object sender, System.EventArgs e)
    {
        chunkCount--;
        if (chunkCount >= 0)
        {
            collectabledChunksList.RemoveAt(chunkCount);
        }

    }



    private void OnCollisionEnter(Collision collision)
    {

        if (chunkCount < maxChunks && collision.gameObject.CompareTag(Constants.GRASS) && state == State.cleaning)
        {
            if (collision.gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }
            collectabledChunksList.Add(collision.transform);
            StartCoroutine(StackCollectedChunksCoroutine(grassOffsetAmount));
            chunkCount++;
        }
        if (state == State.loosing && collision.gameObject.CompareTag(Constants.DIRT) && !Player.Instance.PlayerHasCollectables())
        {

            if (collision.gameObject.TryGetComponent(out MeshRenderer meshRenderer) && !meshRenderer.enabled)
            {

                meshRenderer.enabled = true;
                meshRenderer.transform.localScale = Vector3.zero;
                meshRenderer.transform.DOScale(Vector3.one, 0.5f)
                                        .SetEase(Ease.OutBounce);


            }
        }
        if (state == State.harvesting && collision.gameObject.CompareTag(Constants.DIRT) && chunkCount < maxChunks)
        {
            if (collision.gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }
            Transform dirtTransform = collision.transform;
            
            if (dirtTransform.GetChild(1).gameObject.activeSelf)
            {
                dirtTransform.DOScale(Vector3.zero, 1f).SetEase(Ease.OutBounce).OnComplete(() =>
                {
                    dirtTransform.gameObject.SetActive(false);
                    
                });
                chunkCount++;
                collectabledChunksList.Add(collision.transform.GetChild(1));
                //StackCollectedChunks(tomatoOffsetAmount);
                StartCoroutine(StackCollectedChunksCoroutine(tomatoOffsetAmount));

            }


        }
    }
    //private void StackCollectedChunks(float offsetAmount)
    //{
    //    float yOffset = 0f;
    //    foreach (Transform chunk in collectabledChunksList)
    //    {
    //        if (chunk == null) continue;
    //        chunk.parent = collectablesHoldPoint;
    //        Vector3 targetPosition = collectablesHoldPoint.localPosition + Vector3.up * yOffset;
    //        chunk.DOLocalMove(targetPosition, 1f).SetEase(Ease.OutQuad);
    //        yOffset += offsetAmount;
    //    }
    //}

    private IEnumerator StackCollectedChunksCoroutine(float offsetAmount)
    {
        List<Transform> chunksCopy = new List<Transform>(collectabledChunksList);
        float yOffset = 0f;
        foreach (Transform chunk in chunksCopy)
        {
            if (chunk == null) continue;
            chunk.parent = collectablesHoldPoint;
            Vector3 targetPosition = collectablesHoldPoint.localPosition + Vector3.up * yOffset;
            chunk.DOLocalMove(targetPosition, 1f).SetEase(Ease.OutQuad);
            yOffset += offsetAmount;
            Debug.Log(chunksCopy.Count);
        }
        yield return new WaitForSeconds(0.02f);
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
            this.farmingAreaSatus = null;
        }
    }










}
