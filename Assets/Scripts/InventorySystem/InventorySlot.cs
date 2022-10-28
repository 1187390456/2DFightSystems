using System;
using Unity.VisualScripting;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class InventorySolt
    {
        public event EventHandler<InventorySlotStateChangedArgs> StateChanged; //发布订阅 更改存储库存状态(新状态 是否激活)

        [SerializeField] [Header("物体堆当前状态")] private ItemStack _state;

        private bool _active;

        public bool HasItem => _state?.ItemDefinition != null;
        public ItemDefinition ItemDefinition => _state?.ItemDefinition;

        // 启用状态改变 通知事件调用 更新插槽状态
        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                NotifyAboutStateChange();
            }
        }

        // 物体堆状态改变

        public ItemStack NewState
        {
            get => _state;
            set
            {
                _state = value;
                NotifyAboutStateChange();
            }
        }

        // 数量改变
        public int NumberOfItems
        {
            get => _state.NumberOfItems;
            set
            {
                _state.NumberOfItems = value;
                NotifyAboutStateChange();
            }
        }

        // 通知状态改变事件

        public void NotifyAboutStateChange()
        {
            StateChanged?.Invoke(this, new InventorySlotStateChangedArgs(NewState, Active));
        }

        // 清空库存方法
        public void Clear()
        {
            NewState = null;
        }
    }
}