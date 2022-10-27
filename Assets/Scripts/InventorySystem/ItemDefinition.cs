using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "Item Definition", menuName = "Inventory/Item Definition")]
    public class ItemDefinition : ScriptableObject
    {
        [SerializeField] [Header("物体名称")] private string _name;
        [SerializeField] [Header("是否可堆叠")] private bool _canStack;
        [SerializeField] [Header("游戏显示精灵图")] private Sprite _gameSprite;
        [SerializeField] [Header("UI显示精灵图")] private Sprite _uiSprite;

        public string Name => _name;
        public bool CanStack => _canStack;
        public Sprite GameSprite => _gameSprite;
        public Sprite UISprite => _uiSprite;
    }
}