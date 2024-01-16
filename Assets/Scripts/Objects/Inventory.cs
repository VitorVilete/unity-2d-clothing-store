using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public int space = 10;

    public List<ItemSO> items;

    public bool Add(ItemSO item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough space in inventory");
            return false;
        }
        items.Add(item);
        return true;
    }

    public void Remove(ItemSO item)
    {
        items.Remove(item);
    }
}
