using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("Action events")]
    public UnityEvent onInteract;

    public void Interact()
    {
        if (onInteract != null)
        {
            onInteract.Invoke();
        }
    }
}