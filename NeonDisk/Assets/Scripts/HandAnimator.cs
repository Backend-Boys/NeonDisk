using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimator : MonoBehaviour
{
    private ActionBasedController _controller;
    private Animator _handAnimator;

    private bool _grabbing;

    void Start()
    {
        _controller = GetComponent<ActionBasedController>();
    }

    void LateUpdate()
    {
        _grabbing = _controller.selectAction.action.ReadValue<float>() > 0.5f;

        if (_handAnimator != null)
            _handAnimator.SetBool("Grab", _grabbing);
        else
            _handAnimator = _controller.modelTransform.GetComponentInChildren<Animator>();
    }
}
