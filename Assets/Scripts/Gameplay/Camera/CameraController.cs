using System.Collections;
using System.Collections.Generic;
using AOP.Tools;
using UnityEngine;

namespace AOP.GamePlay.CameraControl
{
    [RequireComponent(typeof(SlideInput))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 CameraTargetPosition;
        private bool isInited;
        private Camera Camera;
        private Vector4 CameraLimit;
        private SlideInput slideInput;

        private void CameraMovement()
        {
            if (!isInited) return;
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, CameraTargetPosition, 0.1f);
        }
        private void Update()
        {
            CameraMovement();
        }

        public void Init(Vector4 cameraLimit,Vector2 Center)
        {
            Camera = Camera.main;
            Camera.transform.position = new Vector3(Center.x, Center.y, Camera.transform.position.z);
            CameraTargetPosition = Camera.transform.position;
            TryGetComponent(out slideInput);
            CameraLimit = cameraLimit;
            slideInput.onSlide = OnScreenSlide;
            isInited = true;
        }

        private void OnScreenSlide(Vector2 Delta)
        {
            CameraTargetPosition.x += Delta.x * .01f;
            CameraTargetPosition.y += Delta.y * .01f;
            CameraTargetPosition.x = Mathf.Clamp(CameraTargetPosition.x, CameraLimit.x, CameraLimit.y);
            CameraTargetPosition.y = Mathf.Clamp(CameraTargetPosition.y, CameraLimit.z, CameraLimit.w);
        }
    }
}

