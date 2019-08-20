﻿using BS;

namespace RapierGun
{
    // This create an item module that can be referenced in the item JSON
    public class ItemModuleRapierGun : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            base.OnItemLoaded(item);
            item.gameObject.AddComponent<ItemRapierGun>();
        }
    }
}
