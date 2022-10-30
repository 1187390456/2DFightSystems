using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.UI
{
    public class UI_InventorySlot : MonoBehaviour
    {
        [SerializeField] [Header("当前引用库存")] private Inventory _inventory;
        [SerializeField] [Header("库存插槽索引")] private int _inventoryslotIndex;

        [SerializeField] [Header("物体图片")] private Image _itemIcon;
        [SerializeField] [Header("激活指示器")] private Image _activeIndicator;
        [SerializeField] [Header("物体数量")] private TMP_Text _numberOfItems;
        private InventorySolt _inventorySolt;

        private void Start()
        {
            AssignSolt(_inventoryslotIndex); // 确保 UI协同工作
        }

        public void AssignSolt(int slotIndex)
        {
            if (_inventorySolt != null) _inventorySolt.StateChanged -= OnStateChanged;
            _inventoryslotIndex = slotIndex;
            if (_inventory == null) _inventory = GetComponentInParent<UI_Inventory>().Inventory;
            _inventorySolt = _inventory.Slots[_inventoryslotIndex];
            _inventorySolt.StateChanged += OnStateChanged;
            UpdateViewState(_inventorySolt.NewState, _inventorySolt.Active);
        }

        private void UpdateViewState(ItemStack state, bool active)
        {
            _activeIndicator.enabled = active;
            var item = state?.ItemDefinition;
            var hasItem = item != null;
            var canStack = hasItem && item.CanStack;
            _itemIcon.enabled = hasItem; // 有物体则显示图片
            _numberOfItems.enabled = canStack; // 能堆叠则显示数量
            if (!hasItem) return;
            _itemIcon.sprite = item.UISprite;
            if (canStack) _numberOfItems.SetText(state.NumberOfItems.ToString());
        }

        private void OnStateChanged(object sender, InventorySlotStateChangedArgs args)
        {
            UpdateViewState(args.NewState, args.Active);
        }
    }
}