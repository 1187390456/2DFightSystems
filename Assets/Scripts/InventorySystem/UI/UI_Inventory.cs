using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;

namespace InventorySystem.UI
{
    public class UI_Inventory : MonoBehaviour
    {
        public static UI_Inventory Instance { get; private set; }
        [SerializeField] [Header("库存插槽预制件")] private GameObject _inventorySlotPrefab;
        [SerializeField] [Header("当前库存")] private Inventory _inventory;
        [SerializeField] [Header("插槽列表")] private List<UI_InventorySlot> _uiInventorySlots;

        public Inventory Inventory => _inventory;

        private void Awake()
        {
            Instance = this;
        }

        [ContextMenu("Init Inventory")]
        private void InitInventoryUi()
        {
            if (_inventory == null && _inventorySlotPrefab == null) return;
            _uiInventorySlots = new List<UI_InventorySlot>(_inventory.Size);
            for (var i = 0; i < _inventory.Size; i++)
            {
                var uiSolt = PrefabUtility.InstantiatePrefab(_inventorySlotPrefab) as GameObject;
                uiSolt.transform.SetParent(transform, false);
                var uiSlotScript = uiSolt.GetComponent<UI_InventorySlot>();
                uiSlotScript.AssignSolt(i);
                _uiInventorySlots.Add(uiSlotScript);
            }
        }

        public void OpenInventory()
        {
            var rect = GetComponent<RectTransform>();
            var posY = rect.rect.height;
            if (gameObject.activeSelf)
            {
                // 关闭
                Player.Instance.inInventorying = false;
                var tween0 = DOTween.To(() => rect.anchoredPosition, x => rect.anchoredPosition = x, new Vector2(0, -posY), .2f);
                tween0.onComplete = () =>
                 {
                     gameObject.SetActive(false);
                 };
            }
            else
            {
                // 打开
                Player.Instance.inInventorying = true;
                gameObject.SetActive(true);
                var tween1 = DOTween.To(() => rect.anchoredPosition, x => rect.anchoredPosition = x, new Vector2(0, 0), .2f);
            }
        }
    }
}