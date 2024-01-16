using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuration")]
    public string interactableTag;

    private Interactable interactable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(interactableTag)) 
        { 
            var collidingInteractable = collision.GetComponent<Interactable>();
            interactable = collidingInteractable;
            interactable.Interact();
        }
        Debug.Log("Can Interact");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(interactableTag))
        {
            interactable = null;
        }
        Debug.Log("Cannot Interact");
    }

    public void EnableInteractable()
    {
        if(interactable != null)
        {
            interactable.Interact();
        }
    }
}
