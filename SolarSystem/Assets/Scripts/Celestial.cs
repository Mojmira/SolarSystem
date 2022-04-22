using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celestial : MonoBehaviour
{
    public Vector3 initialVelocity;

    public string bodyName = "Unnamed";
    public float radius;
    public Vector3 velocity;
    public float mass;
    //public float radius { get; set; }
    //public Vector3 velocity { get; set; }
    //public float mass { get; set; }

    public Rigidbody rb;

    private void Awake()
    {
        velocity = initialVelocity;
    }


    void OnValidate()
    {
        //mass = surfaceGravity * radius * radius / UniverseData.gravitationalConstant;
        //transform.localScale = Vector3.one * radius;
        //gameObject.name = bodyName;
    }

    public void UpdateVelocity(Celestial[] allBodies, float timeStep)
    {
        foreach (var otherBody in allBodies)
        {
            if (otherBody != this)
            {
                float sqrDst = (otherBody.transform.position - transform.position).sqrMagnitude;
                Vector3 forceDir = (otherBody.transform.position - transform.position).normalized;

                Vector3 acceleration = forceDir * UniverseData.gravitationalConstant * otherBody.mass / sqrDst;
                velocity += acceleration * timeStep;
            }
        }
    }

    public void UpdateVelocity(Vector3 acceleration, float timeStep)
    {
        velocity += acceleration * timeStep;
    }

    public void UpdatePosition(float timeStep)
    {
        Debug.Log(bodyName + " velocity: " + velocity);

        rb.MovePosition(rb.position + velocity * timeStep);
        Debug.Log(bodyName + " rb.position: " + rb.position);


    }
}
