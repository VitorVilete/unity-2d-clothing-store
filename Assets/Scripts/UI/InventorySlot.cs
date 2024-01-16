using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    // Start is called before the first frame update
    public Image icon;
    ItemSO item;
    public void AddItem(ItemSO newItem)
    {
        item = newItem;
        icon.sprite = item.sprite;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
