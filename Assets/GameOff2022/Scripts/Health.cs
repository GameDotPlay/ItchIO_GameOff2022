using UnityEngine;

namespace LighterThanAir
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float baseMaxHealth;
        [SerializeField] private float currentMaxHealth;
        [SerializeField] private float currentHealth;

        private void Start()
        {
            this.currentMaxHealth = baseMaxHealth;
            this.currentHealth = currentMaxHealth;
        }

        public float ModifyHealth(float value)
        {
            return this.currentHealth + value;
        }

        public void MaxHealthMultiplier(float value)
        {
            this.currentMaxHealth *= value;
        }

        public void SetMaxHealth(float value)
        {
            this.currentMaxHealth = value;
        }

        public void SetBaseMaxHealth(float value)
        {
            this.baseMaxHealth = value;
        }

        public void SetHealth(float value)
        {
            if (value > this.currentMaxHealth)
            {
                this.currentMaxHealth = value;
            }

            this.currentHealth = value;
        }
    }
}