using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Configuration")]
    public float speed;

    [Header("Dependencies")]
    public Rigidbody2D playerRb;
    public CharacterSO playerCharacter;

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
        Debug.Log("Interact");
    }
}
