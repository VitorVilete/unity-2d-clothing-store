using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    [Header("Configuration")]
    public float speed;

    [Header("Dependencies")]
    public Rigidbody2D playerRb;
    public Body playerCharacterBody;
    public Inventory playerCharacterInventory;
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
            List<ItemSO> clothesInInventory = playerCharacterInventory.items.FindAll(s => s.itemType == "Clothes");
            List<ItemSO> hairsInInventory = playerCharacterInventory.items.FindAll(s => s.itemType == "Hair");
            playerCharacterBody.currentClothes = clothesInInventory[Random.Range(0, clothesInInventory.Count())]; ;
            playerCharacterBody.currentHair = hairsInInventory[Random.Range(0, hairsInInventory.Count())];
            playerAnim.UpdateCharacterSprites();
            Debug.Log("Interact");
        }
    }
}
