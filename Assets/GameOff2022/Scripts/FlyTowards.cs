using UnityEngine;

namespace LighterThanAir
{
    public class FlyTowards : MonoBehaviour
    {
        private enum State
        {
            LockedOnTarget,
            FlyingPastTarget,
            AcquiringTarget
        };

        private enum TurnDirection
        {
            Left = -1,
            Right = 1
        };

        [SerializeField] private Transform targetTransform;
        [SerializeField] private float flyingSpeed = 10.0f;
        [SerializeField] private float yawRate = 2.0f;
        [SerializeField] private float rollRate = 5.0f;
        [SerializeField] private float distanceToFlyPast = 20.0f;
        [SerializeField] private float maxRollAngle = 30.0f;
        [SerializeField] private float tooCloseToAttack = 10.0f;

        private Vector3 yOffset;
        private State currentState = State.AcquiringTarget;
        private TurnDirection nextTurnDirection = TurnDirection.Right;

        void Start()
        {
            yOffset = Vector3.up * transform.position.y;
        }

        void LateUpdate()
        {
            this.SetNextTurnDirection();

            if (this.TooCloseToAttack())
            {
                this.SwitchState(State.FlyingPastTarget);
            }
            
            switch (this.currentState)
            {
                case State.AcquiringTarget:
                    this.AcquireTarget();
                    break;

                case State.LockedOnTarget:
                    break;

                case State.FlyingPastTarget:
                    this.FlyPastTarget();
                    break;

                default:
                    break;
            }

            this.FlyForward();
            this.DrawHelpers();
        }
		
		private void FlyForward()
		{
			this.transform.Translate(Vector3.forward * flyingSpeed * Time.deltaTime, Space.World);
		}

        private void AcquireTarget()
        {
            // Determine which direction to rotate towards
            Vector3 targetDirection = ((targetTransform.position - transform.position) + yOffset).normalized;

            // Get delta angle between current forward vector and the vector towards target.
            float angleDiff = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);
            if (angleDiff <= Mathf.Epsilon)
            {
                SwitchState(State.LockedOnTarget);
                return;
            }

            // The step size is equal to speed times frame time.
            float yawStep = yawRate * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, yawStep, 0.0f);

            // Calculate a rotation a step closer to the target and applies rotation to this object.
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        private void FlyPastTarget()
        {
            float distanceToTarget = ((targetTransform.position - transform.position) + yOffset).magnitude;
            if (distanceToTarget >= distanceToFlyPast)
            {
                this.SwitchState(State.AcquiringTarget);
            }
        }

        private void DrawHelpers()
        {
            Debug.DrawLine(transform.position, targetTransform.position, Color.magenta, Time.deltaTime);
            Debug.DrawLine(transform.position, transform.position + transform.forward * 10, Color.blue, Time.deltaTime);
            Debug.DrawLine(transform.position, transform.position + transform.up * 10, Color.green, Time.deltaTime);
            Debug.DrawLine(transform.position, transform.position + transform.right * 10, Color.red, Time.deltaTime);
        }

        private void SwitchState(State newState)
        {
            this.currentState = newState;
        }

        private bool TooCloseToAttack()
        {
            return Vector3.Distance(targetTransform.position + yOffset, transform.position) < tooCloseToAttack ? true : false;
        }

        private float NormalizeValue(float value, float min, float max)
        {
            return (value - min) / (max - min);
        }

        private void SetNextTurnDirection()
        {
            Vector3 targetDirection = (targetTransform.position - transform.position) + yOffset;
			Vector3 cross = Vector3.Cross(transform.position, targetDirection);
			
			if (cross.z <= 0)
			{
				nextTurnDirection = TurnDirection.Right;
			}
			else
			{
				nextTurnDirection = TurnDirection.Left;
			}
		}
    }
}