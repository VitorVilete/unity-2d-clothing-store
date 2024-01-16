using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    [Header("Configuration")]
    public float speed;

    [Header("Dependencies")]
    public Rigidbody2D playerRb;
    public CharacterSO playerCharacter;
    public PlayerAnimation playerAnim;
    public BodySO customTesteBody;

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
            playerCharacter.body = customTesteBody;
            playerAnim.UpdateCharacterSprites();
            Debug.Log("Interact");
        }
    }
}
