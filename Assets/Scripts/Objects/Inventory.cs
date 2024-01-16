using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public UnityEvent onItemChanged;

    public int space = 9;

    public List<ItemSO> items;

    public List<ItemSO> startingItems;

    private void Start()
    {
        if (startingItems.Count > 0)
        {
            foreach (ItemSO item in startingItems)
            {
                Add(item);
            }
        }
    }

    public bool Add(ItemSO item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Not enough space in inventory");
            return false;
        }
        items.Add(item);
        if(onItemChanged != null)
        {
            onItemChanged.Invoke();
        }
        return true;
    }

    public void Remove(ItemSO item)
    {
        items.Remove(item);
    }
}
