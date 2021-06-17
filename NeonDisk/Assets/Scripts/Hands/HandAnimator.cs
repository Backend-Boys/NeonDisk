using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimator : MonoBehaviour
{
    public InputActionReference point;
    public bool hasItem = false;

    private ActionBasedController _controller;
    private Animator _handAnimator;

    private bool _grabbing;
    private bool _pointing;

    void Start()
    {
        _controller = GetComponent<ActionBasedController>();
    }

    public void IsHolding(bool holding)
    {
        hasItem = holding;
    }

    void LateUpdate()
    {
        _grabbing = _controller.selectAction.action.ReadValue<float>() > 0.5f;
        _pointing = point.action.ReadValue<float>() > 0.5f;

        if (_handAnimator != null)
        {
            if (_grabbing)
            {
                _handAnimator.SetBool("Grab", true);
                _handAnimator.SetBool("Point", false);
            }
            else if (_pointing)
            {
                _handAnimator.SetBool("Grab", false);
                _handAnimator.SetBool("Point", true);
            }
            else
            {
                _handAnimator.SetBool("Grab", false);
                _handAnimator.SetBool("Point", false);
            }

            _handAnimator.SetBool("HasDisk", hasItem);
        }
        else
            _handAnimator = _controller.modelTransform.GetComponentInChildren<Animator>();
    }
}
