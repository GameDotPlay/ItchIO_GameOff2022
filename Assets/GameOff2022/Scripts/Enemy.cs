using UnityEngine;

namespace LighterThanAir
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private float maxFireRange = 50.0f;
        [SerializeField] private float damagePerShot = 3.0f;
        [SerializeField] private float fireRate = 0.2f;

        private AudioSource gunAudio;
        private FlyTowards flyTowardsComponent;
        private float minAudioPitchVariation = 1.1f;
        private float maxAudioPitchVariation = 1.175f;

        private void Start()
        {
            this.gunAudio = GetComponent<AudioSource>();
            this.flyTowardsComponent = GetComponent<FlyTowards>();
        }

        private void Update()
        {
            Vector3 targetPosition = target.transform.position + Vector3.up * this.transform.position.y;
            float distanceToTarget = (targetPosition - this.transform.position).magnitude;

            if (flyTowardsComponent.CurrentState == FlyTowards.State.LockedOnTarget && distanceToTarget <= maxFireRange)
            {
                if (!gunAudio.isPlaying)
                {
                    this.gunAudio.pitch = Random.Range(this.minAudioPitchVariation, this.maxAudioPitchVariation);
                    this.gunAudio.Play();
                }
            }
            else
            {
                this.gunAudio.Stop();
            }
        }
    }
}