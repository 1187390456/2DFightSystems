using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class ItemStack
    {
        [SerializeField] [Header("物体定义信息")] private ItemDefinition _item;
        [SerializeField] [Header("物体数量")] private int _numberOfItems;

        public ItemDefinition Item => _item;

        public bool CanStack => Item != null && _item.CanStack;

        public int NumberOfItems
        {
            get => _numberOfItems;
            set
            {
                value = Mathf.Clamp(value, 0, 999);
                _numberOfItems = CanStack ? value : 1;
            }
        }

        public ItemStack(ItemDefinition item, int numberOfItems)
        {
            _item = item;
            _numberOfItems = numberOfItems;
        }

        public ItemStack()
        { }
    }
}