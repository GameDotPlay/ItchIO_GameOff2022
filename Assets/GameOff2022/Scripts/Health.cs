using UnityEngine;

namespace LighterThanAir
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;

        private void Start()
        {
            this.currentHealth = this.maxHealth;
        }

        public float ModifyHealth(float value)
        {
            return this.currentHealth + value;
        }

        public float ModifyMaxHealth(float value)
        {
            return this.maxHealth + value;
        }
    }
}