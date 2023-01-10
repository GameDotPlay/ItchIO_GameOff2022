using UnityEngine;

namespace LighterThanAir
{
    public class RotatingGun : MonoBehaviour
    {
        private enum TargetModes
        {
            Closest = 0,
            LeastHealth = 1,
            MostHealth = 2
        };

        private RotatingGunMount child = null;
        private GameObject[] enemies = null;
        private GameObject target = null;
        private TargetModes targetMode = TargetModes.Closest;

		private void Awake()
		{
			this.child = (RotatingGunMount)this.GetComponentInChildren(typeof(RotatingGunMount));
			this.InitializeEnemyList();
		}

		private void Start()
        {
			this.GetNewTarget();
		}

		private void LateUpdate()
		{
			if (this.child.TargetDotProduct < this.child.DotProductLimit) 
            {
                this.GetNewTarget();
            }
		}

		private void GetNewTarget()
		{
			switch(this.targetMode)
            {
                case TargetModes.Closest:
                    this.TargetClosest();
                    break;
				case TargetModes.LeastHealth:
                    this.TargetLowestHealth();
					break;
				case TargetModes.MostHealth:
                    this.TargetMostHealth();
					break;
				default:
                    this.TargetClosest();
					break;
			}
		}

		private void SetChildTarget(GameObject target)
        {
            this.child.SetTarget(target);
        }

        private void InitializeEnemyList()
        {
            this.enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }

        private void TargetClosest()
        {
            this.target = GetClosestEnemy();
            this.SetChildTarget(this.target);
        }

        private void TargetLowestHealth()
        {
			this.SetChildTarget(this.target);
		}

        private void TargetMostHealth()
        {
			this.SetChildTarget(this.target);
		}

        private GameObject GetClosestEnemy()
        {
            if (this.enemies == null || this.enemies.Length == 0)
            {
                return null;
            }

            GameObject closest = this.enemies[0];
            float distance = (closest.transform.position - this.transform.position).magnitude;
            for (int i = 1; i < this.enemies.Length; i++)
            {
                float tempDistance = (this.enemies[i].transform.position - this.transform.position).magnitude;
                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    closest = this.enemies[i];
                }
            }

            return closest;
        }
    }
}