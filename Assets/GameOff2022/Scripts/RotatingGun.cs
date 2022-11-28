using UnityEngine;

namespace LighterThanAir
{
    public class RotatingGun : MonoBehaviour
    {
        private RotatingGunMount child = null;
        private GameObject[] enemies = null;
        private GameObject target = null;

        private void Start()
        {
            child = (RotatingGunMount)this.GetComponentInChildren(typeof(RotatingGunMount));
            this.InitializeEnemyList();
        }

        private void Update()
        {
            
        }

        private void SetChildTarget(GameObject target)
        {
            this.child.Target = target;
        }

        private void InitializeEnemyList()
        {
            this.enemies = GameObject.FindGameObjectsWithTag($"Enemy");
        }

        private void TargetClosest()
        {
            this.target = GetClosestEnemy();
        }

        private void TargetLowestHealth()
        {

        }

        private void TargetMostHealth()
        {

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