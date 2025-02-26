using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofreFinal : MonoBehaviour
{
    [SerializeField] int numGemas = 3;
    public int numActualGemas = 0;
    Animator anim;
    [SerializeField] string animNombre = "";
    [SerializeField] GameObject body;
    [SerializeField] AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void gemaColocada()
    {
        numActualGemas++;
        if (numActualGemas == numGemas)
        { 
            anim.Play(animNombre);
            audioSrc.Play();
            body.SetActive(true);
        }
    }
}
