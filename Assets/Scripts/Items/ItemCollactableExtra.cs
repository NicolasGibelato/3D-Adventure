using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

public class ItemCollactableExtra : ItemCollactableBase
{
    protected override void OnExtraCollect()
    {
        base.OnExtraCollect();
        ItemManager.Instance.AddByType(ItemType.LIFE_PACK, 1);
    }
}
