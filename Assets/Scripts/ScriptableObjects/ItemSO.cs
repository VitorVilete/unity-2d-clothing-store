using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public string itemType;
    public string itemPath;
    public int value;
    public Sprite sprite;
}
