using UnityEngine;

namespace LighterThanAir
{
    public class RotatingGunMount : MonoBehaviour
    {
        [SerializeField] private float fireAngleRange = 150.0f;

        public GameObject Target { get; set; }
        public float YawRate { get; set; }

        private Vector3 initialRotation;

        private void Start()
        {
            initialRotation = this.transform.rotation.eulerAngles;
        }

        private void LateUpdate()
        {
            if (Target == null)
            {
                return;
            }

            Vector3 direction = ((Target.transform.position - transform.position) - (Vector3.up * Target.transform.position.y)).normalized;

            Quaternion goalRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, goalRotation, YawRate);

            ClampRotationAngle();
        }

        private void ClampRotationAngle()
        {
            if (transform.rotation.eulerAngles.y - initialRotation.y > fireAngleRange / 2)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, initialRotation.y + (fireAngleRange / 2), transform.rotation.eulerAngles.z);
            }
            else if (initialRotation.y + transform.rotation.eulerAngles.x < fireAngleRange)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, initialRotation.y - (fireAngleRange / 2), transform.rotation.eulerAngles.z);
            }
        }
    }
}