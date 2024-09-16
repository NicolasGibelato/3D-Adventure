using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        public string compareTag = "Player";
        public float duration = 2f;
        private int currentLevel = 1;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }
        }

        public virtual void Collect()
        {
            HideObject();
            var setup = ClothManager.Instance.GetSetupByType(clothType);
            Player.Instance.ChangeTexture(setup, duration);
            SaveManager.Instance.SaveLastLevel(currentLevel);
        }

        protected void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}