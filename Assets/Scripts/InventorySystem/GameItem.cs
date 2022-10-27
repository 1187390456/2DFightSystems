using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class GameItem : MonoBehaviour
    {
        [SerializeField] [Header("物体堆栈信息")] private ItemStack _itemStack;
        [SerializeField] [Header("当前精灵渲染")] private SpriteRenderer _spriteRenderer;

        private void OnValidate()
        {
            SetupGameObject();
        }

        private void SetupGameObject()
        {
            if (_itemStack == null) return;

            _spriteRenderer.sprite = _itemStack.Item.GameSprite;

            _itemStack.NumberOfItems = _itemStack.NumberOfItems;

            var name = _itemStack.Item.Name;
            var number = _itemStack.CanStack ? _itemStack.NumberOfItems.ToString() : "not allow stack";
            gameObject.name = $"{name}({number})";
        }
    }
}