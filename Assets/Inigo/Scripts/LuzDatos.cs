using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzDatos : MonoBehaviour
{
    public Material material;
    public float range;
    public Light l;
    // Start is called before the first frame update
    void Start()
    {
        material.SetFloat("_LightRange", range);
    }

    // Update is called once per frame
    void Update()
    {
        print(transform.position);
        material.SetVector("_LightPos", transform.position);
        material.SetFloat("_LightIntensity", l.intensity/3.5f);
    }
}
