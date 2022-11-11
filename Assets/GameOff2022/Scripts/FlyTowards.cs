using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTowards : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private float flyingSpeed = 10.0f;
    [SerializeField] 
    private float turningSpeed = 2.0f;
    [SerializeField]
    private float distanceToFlyPast = 20.0f;
    [SerializeField]
    private float maxRollRotation = 30.0f;
    private Vector3 targetPosition;
    
    private float distanceToTarget;

    // Start is called before the first frame update
    void Start()
    {
        // Cache transform copy.
        this.targetPosition = this.targetTransform.position;
        this.targetPosition.y = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = targetPosition - transform.position;
        distanceToTarget = targetDirection.magnitude;

        // The step size is equal to speed times frame time.
        float singleStep = turningSpeed * Time.deltaTime;

        // If close to target, fly straight past then turn around for another pass.
        if (distanceToTarget > distanceToFlyPast)
        {
            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            //Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        
        this.transform.Translate(Vector3.forward * flyingSpeed * Time.deltaTime);
    }
}