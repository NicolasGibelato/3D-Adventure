using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructableItemBase : MonoBehaviour
{
    public HealthBase healthBase;
    public float shakeDuration = 0.5f;
    public int shakeForce = 5;

    public int dropCoinsAmount = 2;
    public GameObject coinPrefab;
    public Transform dropPositon;

    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();
        healthBase.OnDamage += OnDamage;
    }

    private void OnDamage(HealthBase h)
    {
        transform.DOShakeScale(shakeDuration, Vector3.up/2, shakeForce);
        DropGroupOfCoins();
    }

    private void DropCoins()
    {
        var i = Instantiate(coinPrefab);
            i.transform.position = dropPositon.position;
        i.transform.DOScale(0, 1f).SetEase(Ease.OutBack).From();
    }

    private void DropGroupOfCoins()
    {
        StartCoroutine(DropGroupOfCoinsCoroutine());
    }

    IEnumerator DropGroupOfCoinsCoroutine()
    {
        for (int i = 0; i < dropCoinsAmount; i++)
        {
            DropCoins();
            yield return new WaitForSeconds(.5f);
        }
    }
}
