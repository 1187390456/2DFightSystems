using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace InventorySystem
{
    public class GameItemSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _itemBasePrefabs;

        public void SpawnItem(ItemStack itemStack)
        {
            if (_itemBasePrefabs == null) return;
            var item = PrefabUtility.InstantiatePrefab(_itemBasePrefabs) as GameObject;
            item.transform.position = transform.position;
            var gameItemScript = item.GetComponent<GameItem>();
            gameItemScript.ItemStack = new ItemStack(itemStack.ItemDefinition, itemStack.NumberOfItems);
            gameItemScript.Throw(Player.Instance.movement.facingDireciton);
        }
    }
}