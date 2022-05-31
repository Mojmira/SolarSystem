using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CelestialController : MonoBehaviour
{

    static CelestialController instance;
    Celestial[] celestials;
    public List<string> celestial_names = new List<string>() { "Sun", "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };
    //public List<float> celestial_sizes = new List<float>() { 109.22f, 0.383f, 0.949f, 1f, 0.532f, 11.21f, 9.14f, 4.01f, 3.88f };
    public List<float> celestial_sizes = new List<float>() { 696f, 2.4f, 6f, 6.3f, 3.4f, 70f, 58f, 25f, 24.6f };

    //public List<float> celestial_distances = new List<float>() {0f, 57.9f, 108.2f, 149.6f, 227.9f, 778.6f, 1433.5f, 2872.5f, 4495.1f};
    //public List<float> celestial_distances = new List<float>() { 0, 0.387f, 0.723f, 1, 1.52f, 5.2f, 9.57f, 19.17f, 30.18f };
    public List<float> celestial_distances = new List<float>() { 0, 63.81f, 107.59f, 151.48f, 248.84f, 755.91f, 1487.8f, 2954.6f, 4475.5f };

    //public List<float> celestial_masses = new List<float>() {332.900f,	0.055f,	0.815f,	1,	0.107f,	318f,	95,	14, 17};
    public List<float> celestial_masses = new List<float>() { 333000f, 0.055f, 0.815f, 1, 0.107f, 318f, 95, 14, 17 };

    float sun_distance = 0;


    void Start()
    {
        Time.fixedDeltaTime = UniverseData.physicsTimeStep;
        for (int i =0; i<celestial_names.Count; i++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sun_distance = celestial_distances[i];
            var celestial_diameter = celestial_sizes[i];
            sphere.transform.localScale = new Vector3(celestial_diameter, celestial_diameter, celestial_diameter);
            sphere.transform.position = new Vector3(0, 0, sun_distance);
            sphere.AddComponent<SphereCollider>();
            sphere.AddComponent<Rigidbody>();
            sphere.AddComponent<Celestial>();
            sphere.GetComponent<Celestial>().rb = sphere.GetComponent<Rigidbody>();
            sphere.GetComponent<Celestial>().rb.useGravity = false;

            sphere.GetComponent<Celestial>().bodyName = celestial_names[i];
            sphere.GetComponent<Celestial>().mass = celestial_masses[i] * 100;
            sphere.GetComponent<Celestial>().radius = celestial_sizes[i];
            sphere.name = celestial_names[i];

            Material newMaterial = new Material(Shader.Find("Standard"));

            var texture = Resources.Load<Texture2D>("Textures/" + celestial_names[i]);
            newMaterial.SetTexture("_MainTex", texture);
            sphere.GetComponent<MeshRenderer>().material = newMaterial;

            //celestials.Add(sphere);
        }
        celestials = FindObjectsOfType<Celestial>();
        InitialVelocity();
        foreach (Celestial a in celestials)
        {
            Debug.Log(a.velocity);
        }
        
    }


    void FixedUpdate()
    {
        //for (int i = 0; i < celestials_arr.Length; i++)
        //{
        //    Debug.Log("Celestial position: " + celestials_arr[i].rb.position);

        //    Vector3 acceleration = CalculateAcceleration(celestials_arr[i].rb.position, celestials_arr[i]);
        //    celestials_arr[i].UpdateVelocity(acceleration, UniverseData.physicsTimeStep);
        //    Debug.Log("Calculating acceleration");
        //    Debug.Log(acceleration);


        //    //bodies[i].UpdateVelocity (bodies, Universe.physicsTimeStep);
        //}

        //for (int i = 0; i < celestials_arr.Length; i++)
        //{
        //    var cel_vel = celestials_arr[i].velocity;
        //    //celestials[i].transform.Translate(Vector2.up * cel_vel * UniverseData.physicsTimeStep); 
        //    celestials_arr[i].UpdatePosition(UniverseData.physicsTimeStep);
        //}
        Gravity();


    }

    void Gravity()
    {
        Vector3 acceleration = Vector3.zero;

        foreach (Celestial a in celestials)
        {
            foreach(Celestial b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m1 = a.mass;
                    float m2 = b.mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);
                    Vector3 forceDir = (b.transform.position - a.transform.position).normalized;

                    acceleration += forceDir * (UniverseData.gravitationalConstant * (m1 * m2)/ (r * r));
                    //a.UpdateVelocity(acceleration, UniverseData.physicsTimeStep);
                    //a.UpdatePosition(UniverseData.physicsTimeStep);



                }
            }
            a.UpdateVelocity(acceleration, UniverseData.physicsTimeStep);
            a.UpdatePosition(UniverseData.physicsTimeStep);
        }
    }

    void InitialVelocity()
    {
        foreach (Celestial a in celestials)
        {
            foreach (Celestial b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m2 = b.mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);
                    a.transform.LookAt(b.transform);
                    a.velocity += a.transform.right * Mathf.Sqrt((UniverseData.gravitationalConstant * m2) / r);
                }
            }
        }

    }



    public static Vector3 CalculateAcceleration(Vector3 point, Celestial ignoreBody = null)
    {
        Debug.Log("Point: " + point.ToString());


        Vector3 acceleration = Vector3.zero;
        foreach (var body in Instance.celestials)
        {
            Debug.Log("Body: " + body.bodyName);
            if (body != ignoreBody)
            {
                float sqrDst = (body.rb.position - point).sqrMagnitude;
                Debug.Log("sqrDstr: " + sqrDst);

                Vector3 forceDir = (body.rb.position - point).normalized;
                Debug.Log("ForceDir: " + forceDir);

                //wypadkowa przyspieszen
                acceleration += forceDir * UniverseData.gravitationalConstant * body.mass / sqrDst;

            }
            Debug.Log(body.name + " mass: " + body.mass);
            
        }
        return acceleration;
    }

    static CelestialController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CelestialController>();
            }
            return instance;
        }
    }
}
