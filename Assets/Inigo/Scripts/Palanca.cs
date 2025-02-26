using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palanca : MonoBehaviour
{
    [SerializeField] HingeJoint joint;
    public float activationAngle = 70;
    [SerializeField] ParticleSystem part;
    bool usedPalanca = false;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider col;
    [SerializeField] AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (joint.angle > activationAngle)
        {
            if (!part.isPlaying)
            {
                part.Play();
                audioSrc.Play();
                if (!usedPalanca)
                {
                    rb.isKinematic = false;
                    rb.AddForce(rb.transform.up * 30, ForceMode.Impulse);
                    col.enabled = true;
                    usedPalanca = true;

                }
            }
        }
        else
        {
            if (part.isPlaying)
            {
                audioSrc.Stop();
                part.Stop();
            }
        }
    }
}
