using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Items
{
    public class ItemCollactableBase : MonoBehaviour
    {
        public ItemType itemType;


        public string compareTag = "Player";
        public new ParticleSystem particleSystem;
        public float timeToHide = 3f;
        public GameObject graphicItem;

        public Collider collider;


        [Header("sound")]
        public AudioSource audioSource;

        private void Awake()
        {
            //if (particleSystem != null) particleSystem.transform.SetParent(null);
        }


        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }



        protected virtual void HideItens()
        {
            if (graphicItem != null) graphicItem.SetActive(false);
            Invoke("HideObject", timeToHide);

        }

        protected virtual void Collect()
        {
            if (collider != null) collider.enabled = false;
            HideItens();
            OnCollect();
        }

        protected void HideObject()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnCollect()
        {
            if (particleSystem != null)
            {
                particleSystem.transform.SetParent(null);
                particleSystem.Play();
            }
            ItemManager.Instance.AddByType(itemType);

            if (audioSource != null) audioSource.Play();
        }
        protected virtual void OnExtraCollect() { }

    }
}