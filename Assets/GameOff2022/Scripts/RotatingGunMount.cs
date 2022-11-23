using UnityEngine;

namespace LighterThanAir
{
    public class RotatingGunMount : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private float yawRate = 3.0f;
        [SerializeField] private float fireAngleRange = 150.0f;

        private Vector3 initialRotation;

        private void Start()
        {
            initialRotation = this.transform.rotation.eulerAngles;
        }

        private void Update()
        {

        }

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            Vector3 direction = ((target.transform.position - transform.position) - (Vector3.up * target.transform.position.y)).normalized;

            Quaternion goalRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, goalRotation, yawRate);

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