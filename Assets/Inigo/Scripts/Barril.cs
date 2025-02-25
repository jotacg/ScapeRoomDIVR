using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Barril : MonoBehaviour
{
    bool grabed = false;
    [SerializeField] int layerAcambiar = 0;
    protected int vecesAgarrado = 0;
    int layerInicial;
    // Start is called before the first frame update
    void Start()
    {
        layerInicial = gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLayer()
    {
        grabed = !grabed;
        if (grabed)
        {
            gameObject.layer = layerAcambiar;
        }
        else
        {
            gameObject.layer = layerInicial;
        }
    }

    public void ChangeLayerBool(bool a)
    {
        if (a)
        {
            vecesAgarrado++;
        }
        else
        {
            vecesAgarrado--;
        }
        if (vecesAgarrado!=0)
        {
            grabed = true;

            gameObject.layer = layerAcambiar;
        }
        else
        {
            grabed = false;

            gameObject.layer = layerInicial;
        }
    }
}
