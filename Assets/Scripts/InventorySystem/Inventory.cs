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

        private int _activeSlotIndex;

        public int ActiveSlotIndex
        {
            get => _activeSlotIndex;
            private set
            {
                _slots[_activeSlotIndex].Active = false;
                _activeSlotIndex = value < 0 ? _size - 1 : value % _size; // 小于0 则为 最后一个 否则 返回余数
                _slots[_activeSlotIndex].Active = true;
            }
        }

        private void OnValidate()
        {
            AdjustSize();
        }

        private void Awake()
        {
            if (_size > 0)
            {
                _slots[0].Active = true;
            }
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

        // 是否拥有指定数量的物体
        public bool HasItem(ItemStack itemStack, bool checkNumberOfItems = false)
        {
            var itemSlot = FindSolt(itemStack.ItemDefinition);
            if (itemSlot == null) return false;
            if (!checkNumberOfItems) return true;
            // 如果可堆叠 检测插槽存放物体的数量 是否大于指定物品数量
            if (itemStack.ItemDefinition.CanStack)
            {
                return itemSlot.NumberOfItems >= itemStack.NumberOfItems;
            }
            // 不可堆叠 检测存在多少个该物体 然后计算数量判断是否大于指定物品数量
            return _slots.Count(x => x.ItemDefinition == itemStack.ItemDefinition) >= itemStack.NumberOfItems;
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

        public ItemStack RemoveItem(int index, bool spawn = false)
        {
            if (!_slots[index].HasItem) throw new InventoryException(InventoryOperation.Remove, "Slot is Empty !");
            if (spawn && TryGetComponent<GameItemSpawner>(out var itemSpawner))
            {
                itemSpawner.SpawnItem(_slots[index].NewState);
            }
            ClearSlot(index);
            return new ItemStack();
        }

        public ItemStack RemoveItem(ItemStack itemStack)
        {
            var itemSlot = FindSolt(itemStack.ItemDefinition);
            // 移除物体不存在
            if (itemSlot == null) throw new InventoryException(InventoryOperation.Remove, "Not Item in the  Inventory !");
            // 移除物体数量大于存储数量
            if (itemSlot.ItemDefinition.CanStack && itemSlot.NumberOfItems < itemStack.NumberOfItems)
                throw new InventoryException(InventoryOperation.Remove, "Not enough Items");
            // 移除物品数量 可堆叠且数量大于0时 返回当前物品 否则清空
            itemSlot.NumberOfItems -= itemStack.NumberOfItems;
            if (itemSlot.ItemDefinition.CanStack && itemSlot.NumberOfItems > 0)
            {
                return itemSlot.NewState;
            }
            itemSlot.Clear();

            return new ItemStack();
        }

        public void ClearSlot(int index)
        {
            _slots[index].Clear();
        }

        public void SetSlotIndex(int index)
        {
            ActiveSlotIndex = index;
        }

        public InventorySolt GetInventorySolt()
        {
            return _slots[ActiveSlotIndex];
        }
    }
}