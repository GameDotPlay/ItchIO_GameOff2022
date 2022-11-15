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

        void Update()
        {
            SetNextTurnDirection();

            if (TooCloseToAttack())
            {
                SwitchState(State.FlyingPastTarget);
            }
            
            switch (currentState)
            {
                case State.AcquiringTarget:
                    AcquireTarget();
                    break;

                case State.LockedOnTarget:
                    FlyAtTarget();
                    break;

                case State.FlyingPastTarget:
                    FlyPastTarget();
                    break;

                default:
                    break;
            }

            // Fly forward.
            this.transform.Translate(Vector3.forward * flyingSpeed * Time.deltaTime);

            DrawHelpers();
        }

        private void AcquireTarget()
        {
            // Determine which direction to rotate towards
            Vector3 targetDirection = (targetTransform.position - transform.position) + yOffset;

            // Get delta angle between current forward vector and the vector towards target.
            float angleDiff = Vector3.Angle(transform.forward, targetDirection);
            if (angleDiff < Mathf.Epsilon)
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

        private void FlyAtTarget()
        {
            
        }

        private void FlyPastTarget()
        {
            float distanceToTarget = ((targetTransform.position - transform.position) + yOffset).magnitude;
            if (distanceToTarget > distanceToFlyPast)
            {
                SwitchState(State.AcquiringTarget);
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
            currentState = newState;
        }

        private bool TooCloseToAttack()
        {
            float distanceToTarget = ((targetTransform.position - transform.position) + yOffset).magnitude;

            return distanceToTarget < tooCloseToAttack ? true : false;
        }

        private float NormalizeValue(float value, float min, float max)
        {
            return (value - min) / (max - min);
        }

        private void SetNextTurnDirection()
        {
            if (transform.rotation.eulerAngles.y >= 180.0f)
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