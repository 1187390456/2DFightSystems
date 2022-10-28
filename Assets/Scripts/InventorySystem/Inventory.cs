using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] [Header("库存大小")] private int _size;
        [SerializeField] [Header("插槽列表")] private List<InventorySolt> _slots;

        public List<InventorySolt> Slots => _slots;
        public int Size => _size;

        private void OnValidate()
        {
            AdjustSize();
        }

        // 自动调整列表大小
        private void AdjustSize()
        {
            _slots ??= new List<InventorySolt>();
            if (_slots.Count > _size) _slots.RemoveRange(_size, _slots.Count - _size);
            if (_slots.Count < _size) _slots.AddRange(new InventorySolt[_size - _slots.Count]);
        }

        // 判断库存是否已满 list.Count() 返回指定条件存在的个数
        public bool IsFull() => _slots.Count(x => x.HasItem) >= _size;

        // 能否接收物体 库存未满 或者 当前物体插槽中存在
        public bool CanAcceptItem(ItemStack itemStack)
        {
            var slotWithCanStackItem = FindSolt(itemStack.ItemDefinition, true);
            return !IsFull() || slotWithCanStackItem != null;
        }

        /// <summary>
        /// 寻找指定物体的插槽
        /// </summary>
        /// <param name="item">当前物体信息</param>
        /// <param name="onlyCanStack">是否只寻找可堆叠的物品</param>
        /// <returns></returns>
        public InventorySolt FindSolt(ItemDefinition itemDefinition, bool onlyCanStack = false) => _slots.FirstOrDefault(x => x.ItemDefinition == itemDefinition && (itemDefinition.CanStack || !onlyCanStack));

        public ItemStack AddItem(ItemStack itemStack)
        {
            var relevantSlot = FindSolt(itemStack.ItemDefinition, true);
            // 库存满了 并且没有找到已存放该物品的插槽
            if (IsFull() && relevantSlot == null) throw new InventoryException(InventoryOperation.Add, "库存已满!");
            // 库存没满
            if (relevantSlot != null)
            {
                // 找到相关插槽
                relevantSlot.NumberOfItems += itemStack.NumberOfItems; // 更新数量
            }
            else
            {
                // 开启新插槽(找到一个当前未存储物体的插槽) 存储当前新状态
                relevantSlot = _slots.First(x => !x.HasItem);
                relevantSlot.NewState = itemStack;
            }
            return relevantSlot.NewState;
        }
    }
}