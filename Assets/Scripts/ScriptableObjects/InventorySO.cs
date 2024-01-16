using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Character/Inventory")]
public class InventorySO : ScriptableObject
{
    public List<ItemSO> items;
}
