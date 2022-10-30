using System;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class ItemStack
    {
        [SerializeField] [Header("物体定义信息")] private ItemDefinition _itemDefinition;
        [SerializeField] [Header("物体数量")] private int _numberOfItems;

        // 构造物体
        public ItemStack(ItemDefinition item, int numberOfItems)
        {
            _itemDefinition = item;
            _numberOfItems = numberOfItems;
        }

        public ItemDefinition ItemDefinition => _itemDefinition;

        public bool CanStack => _itemDefinition != null && _itemDefinition.CanStack;

        public int NumberOfItems
        {
            get => _numberOfItems;
            set
            {
                value = Mathf.Clamp(value, 0, 999);
                _numberOfItems = CanStack ? value : 1;
            }
        }

        public ItemStack()
        { }
    }
}