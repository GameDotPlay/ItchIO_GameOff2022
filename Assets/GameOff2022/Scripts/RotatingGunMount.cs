using UnityEngine;

namespace LighterThanAir
{
    public class RotatingGunMount : MonoBehaviour
    {
        #region Property Panel
        [SerializeField] private GameObject target;
		[SerializeField] private float fireAngleRange = 80.0f;
		[SerializeField] private float YawRate = 1.0f;
        [SerializeField] private bool clampRotation = true;
        #endregion

        #region Public 
        public float TargetDotProduct { get; private set; }
		public float DotProductLimit { get; private set; }
		#endregion

		#region Private
		private GunBarrels child = null;
		private Vector3 initialRotation;
        private Vector3 initialForward;
        
		#endregion

		private void Awake()
		{
			this.child = (GunBarrels)this.GetComponentInChildren(typeof(GunBarrels));
		}

		private void Start()
        {
			this.initialRotation = this.transform.rotation.eulerAngles;
            this.initialForward = this.transform.forward;
            Transform maxAngleTransform = this.transform;
            maxAngleTransform.rotation = Quaternion.Euler(this.initialRotation.x, this.initialRotation.y + this.fireAngleRange, this.initialRotation.z);
			this.DotProductLimit = Vector3.Dot(this.initialForward, maxAngleTransform.forward);
        }

        private void LateUpdate()
        {
            if (this.target == null)
            {
                return;
            }

            Vector3 lastRotation = this.transform.rotation.eulerAngles;
            Vector3 direction = (this.target.transform.position - this.transform.position).normalized;

            Quaternion goalRotation = Quaternion.LookRotation(direction);

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, goalRotation, YawRate);
            this.transform.rotation = Quaternion.Euler(this.initialRotation.x, this.transform.rotation.eulerAngles.y, this.initialRotation.z);

            this.TargetDotProduct = Vector3.Dot(this.initialForward, direction);
            Debug.Log(TargetDotProduct);

			if (this.clampRotation)
            {
				this.ClampRotationAngle(lastRotation);
			}
        }

		private void ClampRotationAngle(Vector3 lastRotation)
        {
            if (this.TargetDotProduct < this.DotProductLimit)
            {
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, lastRotation.y, this.transform.rotation.eulerAngles.z);
            }
        }

        public void SetTarget(GameObject target)
        {
            this.target = target;
            this.child.SetTarget(target);
        }
    }
}