using System.Collections.Generic;
using UnityEngine;

namespace LighterThanAir
{
    public class GunBarrels : MonoBehaviour
    {
        [SerializeField] private float fireAngleRange = 80.0f;

        public GameObject Target { get; set; }
        public float PitchRate { get; set; }
        
        private Vector3 initialRotation;

        private void Start()
        {
            initialRotation = transform.rotation.eulerAngles;
        }

        private void LateUpdate()
        {
            if (Target == null)
            {
                return;
            }

            Vector3 inheritedRotation = transform.rotation.eulerAngles;
            Vector3 direction = (Target.transform.position - transform.position).normalized;

            Quaternion goalRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, goalRotation, PitchRate);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, inheritedRotation.y, inheritedRotation.z);

            Debug.Log(transform.rotation.eulerAngles.x);
            ClampRotationAngle();
        }

        private void ClampRotationAngle()
        {
            if (transform.rotation.eulerAngles.x - initialRotation.x > fireAngleRange / 2)
            {
                transform.rotation = Quaternion.Euler(initialRotation.x + (fireAngleRange / 2), transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }
            else if (initialRotation.y + transform.rotation.eulerAngles.x < fireAngleRange)
            {
                transform.rotation = Quaternion.Euler(initialRotation.x - (fireAngleRange / 2), transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }
        }
    }
}