using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace NeonDiskVR.Menus
{
    public class MenuUIInteractor : MonoBehaviour
    {
        [Tooltip("This is used for how fast it will lerp the objects interactions.")]
        public float smoothing;

        [Tooltip("Where do you want the UI to move to when hovered?")]
        public Transform desiredPosition;

        [Tooltip("Where do we return too? Will use spawn pos if null.")]
        public Transform returnPosition = null;

        public bool hideOnReturn = false;

        private Vector3 _originalPos;
        private Vector3 _originalTargetPos;
        private Vector3 _targetPos;
        private bool _moving = false;

        public void Start()
        {
            _originalPos = (returnPosition != null ? returnPosition.position : transform.position);
            _originalTargetPos = desiredPosition.position;

            Return();
        }

        public void Update()
        {
            if (hideOnReturn && _targetPos != _originalTargetPos)
            {
                if (Vector3.Distance(transform.position, _targetPos) < 1)
                    gameObject.SetActive(false);
            }

            if (!_moving)
            {

                return;
            }

            transform.position = Vector3.Lerp(transform.position, _targetPos, Time.deltaTime * smoothing);

            if (transform.position == _targetPos)
                _moving = false;
        }

        public void ResetPosition()
        {
            transform.position = _originalPos;
            gameObject.SetActive(false);

            _moving = false;
        }

        public void Goto()
        {
            _moving = true;
            _targetPos = _originalTargetPos;
        }

        public void Return()
        {
            _moving = true;
            _targetPos = _originalPos;
        }
    }
}
