using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celestial : MonoBehaviour
{
    public Vector3 initialVelocity;

    public string bodyName = "Unnamed";
    public float radius { get; set; }
    public Vector3 velocity { get; set; }
    public float mass { get; set; }




    void OnValidate()
    {
        //mass = surfaceGravity * radius * radius / UniverseData.gravitationalConstant;
        //transform.localScale = Vector3.one * radius;
        Debug.Log("fjepowfew");
        //gameObject.name = bodyName;
    }
}
