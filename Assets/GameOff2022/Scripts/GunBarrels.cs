using System.Collections.Generic;
using UnityEngine;

namespace LighterThanAir
{
    public class GunBarrels : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private float pitchRate = 3.0f;
        [SerializeField] private float fireAngleRange = 80.0f;

        private List<GameObject> enemiesInRange = new List<GameObject>();
        private Vector3 initialRotation;

        private void Start()
        {
            initialRotation = transform.rotation.eulerAngles;
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

            Vector3 inheritedRotation = transform.rotation.eulerAngles;
            Vector3 direction = (target.transform.position - transform.position).normalized;

            Quaternion goalRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, goalRotation, pitchRate);
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