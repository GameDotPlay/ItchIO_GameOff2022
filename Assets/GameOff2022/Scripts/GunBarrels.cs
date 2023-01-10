using System.Collections.Generic;
using UnityEngine;

namespace LighterThanAir
{
    public class GunBarrels : MonoBehaviour
    {
		#region Property Panel
		[SerializeField] private GameObject target;
		[SerializeField] private float fireAngleRange = 45.0f;
		[SerializeField] private float pitchRate = 1.0f;
		[SerializeField] private bool clampRotation = true;
		#endregion

		#region Public
		#endregion

		#region Private
		private Vector3 initialRotation;
		private Vector3 initialForward;
		private float dotProductLimit;
		#endregion

		private void Start()
        {
			this.initialRotation = this.transform.rotation.eulerAngles;
			this.initialForward = this.transform.forward;
			Transform maxAngleTransform = this.transform;
			maxAngleTransform.rotation = Quaternion.Euler(this.initialRotation.x + this.fireAngleRange, this.initialRotation.y, this.initialRotation.z);

			this.dotProductLimit = Vector3.Dot(this.initialForward, maxAngleTransform.forward);
		}

        private void LateUpdate()
        {
            if (this.target == null)
            {
                return;
            }

            Vector3 inheritedRotation = this.transform.rotation.eulerAngles;
			Vector3 direction = (this.target.transform.position - this.transform.position).normalized;

            Quaternion goalRotation = Quaternion.LookRotation(direction);

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, goalRotation, this.pitchRate);
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, inheritedRotation.y, inheritedRotation.z);

            this.ClampRotationAngle(inheritedRotation, direction);
        }

        private void ClampRotationAngle(Vector3 lastRotation, Vector3 direction)
        {
			if (Vector3.Dot(this.initialForward, direction) < this.dotProductLimit)
			{
				this.transform.rotation = Quaternion.Euler(lastRotation.x, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
			}
		}

        public void SetTarget(GameObject target)
        {
			this.target = target;
        }
    }
}