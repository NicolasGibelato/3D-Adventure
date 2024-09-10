using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Items
{
    public class ActionLifePack : MonoBehaviour
    {
        public KeyCode keyCode = KeyCode.H;
        public SOInt sOInt;

        private void Start()
        {
           sOInt =  ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;
        }

        private void RecoverLife()
        {
            if(sOInt.value > 0)
            {
                ItemManager.Instance.RemoveByType(ItemType.LIFE_PACK);
                Player.Instance.healthBase.ResetLife();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(keyCode))
            {
                RecoverLife();
            }
        }
    }
}