using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

namespace LighterThanAir
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float mouseScrollSensitivity = 1.0f;
        [SerializeField] private float minimumCameraDistance = 150.0f;
        [SerializeField] private float maximumCameraDistance = 300.0f;

        private CinemachineVirtualCamera vcam;
        private CinemachineFramingTransposer framingTransposer;
        private Controls controls;

        private float mouseScrollY;

        private void Awake()
        {
            vcam = FindObjectOfType<CinemachineVirtualCamera>();
            framingTransposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
            controls = new Controls();

            controls.Player.MouseScrollY.performed += x => mouseScrollY = x.ReadValue<float>();
        }

        void Start()
        {
            
        }

        void Update()
        {
            if (framingTransposer != null)
            {
                if (mouseScrollY > 0)
                {
                    framingTransposer.m_CameraDistance -= mouseScrollSensitivity;
                }
                else if (mouseScrollY < 0)
                {
                    framingTransposer.m_CameraDistance += mouseScrollSensitivity;
                }

                this.ClampCameraDistance();
            }
        }

        private void ClampCameraDistance()
        {
            if (framingTransposer.m_CameraDistance < minimumCameraDistance)
            {
                framingTransposer.m_CameraDistance = minimumCameraDistance;
            }

            if (framingTransposer.m_CameraDistance > maximumCameraDistance)
            {
                framingTransposer.m_CameraDistance = maximumCameraDistance;
            }
        }

        #region Enable/Disable

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        #endregion
    }
}