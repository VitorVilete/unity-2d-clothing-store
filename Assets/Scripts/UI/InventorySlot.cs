using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    // Start is called before the first frame update
    public Image icon;
    ItemSO item;
    public UnityEvent<ItemSO> onItemEquipped;
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

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void OnEquipButton()
    {
        if (onItemEquipped != null)
        {
            onItemEquipped.Invoke(item);
        }
    }
}
