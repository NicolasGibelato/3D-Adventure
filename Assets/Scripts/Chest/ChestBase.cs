using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    public SFXType sfxType;
    public KeyCode keyCode = KeyCode.E;
    public Animator animator;
    public string triggerOpen = "Open";
    public ChestItemBase chestItem;

    [Header("Notification")]
    public GameObject notification;
    public Tween _currentTween;
    public float tweenDuration = .2f;
    public Ease tweenEase = Ease.OutBack;

    
    private float startScale;
    private bool _chestOpening = false;

    private void Start()
    {
        startScale = notification.transform.localScale.x;
        HideNotification();
    }

    [NaughtyAttributes.Button]
   private void OpenChest()
    {
        if (_chestOpening) return;
        animator.SetTrigger(triggerOpen);
        _chestOpening = true;
        HideNotification();
        PlaySFX();
        Invoke(nameof(ShowItem), 1f);
    }

    private void ShowItem()
    {
        chestItem.ShowItem();
        Invoke(nameof(CollectItem), 1f);
    }

    private void CollectItem()
    {
        chestItem.Collect();
    }

    public void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();
        if(p != null)
        {
            ShowNotification();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();
        if (p != null)
        {
            HideNotification();
        }
    }

    private void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(startScale, tweenDuration);

    }

    private void HideNotification()
    {
        notification.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode) && notification.activeSelf)
        {
            OpenChest();
        }
    }

    private void PlaySFX()
    {
        SFXPool.Instance.Play(sfxType);
    }
}
