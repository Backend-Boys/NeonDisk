using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class TeleportActivate : MonoBehaviour
{
    // Access the GameObject that contains the teleport controller script.
    public GameObject teleportController;

    // Access the GameObject that contains the teleport controller script.
    public InputActionReference teleportActivateReference;

    [Space]
    [Header("Teleport Events")]

    // These will group Unity event calls that you can add to in the inspector.
    public UnityEvent onTeleportActive;
    public UnityEvent onTeleportCancel;

    void Start()
    {
        // An Interaction with the teleportACtivationReference has been completed and performs a callback to the TeleportModeActive.
        teleportActivateReference.action.performed += TeleportModeActivate;

        // An Interaction with the teleportACtivationReference has been cancelled and performs a callback to the TeleportModeActive.
        teleportActivateReference.action.canceled += TeleportModeCancel;
    }

    // This will let us call a series of events created in the onTeleportActive events in the inspector.
    private void TeleportModeActivate(InputAction.CallbackContext obj) => onTeleportActive.Invoke();

    // This will delay the call of the DelayTeleportation function for 0.1 of a second.
    private void TeleportModeCancel(InputAction.CallbackContext obj) => Invoke("DelayTeleportation ", .1f);

    private void DelayTeleportation() => onTeleportCancel.Invoke();


}
