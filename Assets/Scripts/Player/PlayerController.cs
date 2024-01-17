using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Configuration")]
    public float speed;

    [Header("Dependencies")]
    public Rigidbody2D playerRb;
    public Body playerCharacterBody;
    public PlayerAnimation playerAnim;

    private Vector2 _movementInput;

    public void Start()
    {
    }

    private void FixedUpdate()
    {
        playerRb.velocity = _movementInput * speed;
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        _movementInput = value.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            //List<ItemSO> clothesInInventory = Inventory.instance.items.FindAll(s => s.itemType == "Clothes");
            //List<ItemSO> hairsInInventory = Inventory.instance.items.FindAll(s => s.itemType == "Hair");
            //playerCharacterBody.currentClothes = clothesInInventory[Random.Range(0, clothesInInventory.Count())];
            //Debug.Log("Choosen clothes: " + playerCharacterBody.currentClothes.itemName);
            //playerCharacterBody.currentHair = hairsInInventory[Random.Range(0, hairsInInventory.Count())];
            //Debug.Log("Choosen hair: " + playerCharacterBody.currentHair.itemName);
            playerAnim.UpdateCharacterSprites();
            //Debug.Log("Interact");
            Inventory.instance.Add(new ItemSO { itemName = "teste1", itemType = "Clothes", itemDescription = "ItemTestDesc"});
            Debug.Log(Inventory.instance.items.Count);
        }
    }
}
