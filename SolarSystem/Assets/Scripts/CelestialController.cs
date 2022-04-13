using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CelestialController : MonoBehaviour
{
    public List<GameObject> celestials;
    public List<string> celestial_names = new List<string>() { "Sun", "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };
    public List<float> celestial_sizes = new List<float>() { 109.22f, 0.383f, 0.949f, 1f, 0.532f, 11.21f, 9.14f,   4.01f, 3.88f};
    //public List<float> celestial_distances = new List<float>() {0f, 57.9f, 108.2f, 149.6f, 227.9f, 778.6f, 1433.5f, 2872.5f, 4495.1f};
    public List<float> celestial_distances = new List<float>() { 0, 0.387f, 0.723f, 1, 0.00257f, 1.52f, 5.2f, 9.57f, 19.17f, 30.18f };


    float sun_distance = 0;
    void Awake()
    {
        for (int i =0; i<celestial_names.Count; i++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sun_distance = celestial_distances[i] *300;
            var celestial_diameter = celestial_sizes[i];
            Debug.Log(sun_distance);
            sphere.transform.localScale = new Vector3(celestial_diameter, celestial_diameter, celestial_diameter);
            sphere.transform.position = new Vector3(0, 0, sun_distance);
            sphere.AddComponent<SphereCollider>();
            sphere.AddComponent<Celestial>();
            sphere.GetComponent<Celestial>().bodyName = celestial_names[i];
            sphere.GetComponent<Celestial>().mass = 1;
            sphere.GetComponent<Celestial>().radius = celestial_sizes[i];
            sphere.name = celestial_names[i];

            Material newMaterial = new Material(Shader.Find("Standard"));

            var texture = Resources.Load<Texture2D>("Textures/" + celestial_names[i]);
            newMaterial.SetTexture("_MainTex", texture);
            sphere.GetComponent<MeshRenderer>().material = newMaterial;

            celestials.Add(sphere);
        }

    }
}
