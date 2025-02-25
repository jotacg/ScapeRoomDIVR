using System.Collections;
using UnityEngine;

public class Espada : Barril
{
    [SerializeField] private Material endMaterial;
    private Material initialMaterial;
    private Material currentMaterial; // Instancia del material
    private MeshRenderer meshRenderer;
    [SerializeField] private Light lightSword;
    [SerializeField] private float maxLight;

    private Coroutine actualCoroutine;
    public float t = 0f; // Progreso de interpolación

    [SerializeField] private float lerpDuration = 1.5f;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        // Crear una instancia del material para modificarlo sin afectar materiales globales
        currentMaterial = new Material(meshRenderer.material);
        meshRenderer.material = currentMaterial;

        initialMaterial = new Material(currentMaterial);
    }

    public void activarIluminacion()
    {
        if (base.vecesAgarrado == 2)
        {
            if (actualCoroutine != null)
                StopCoroutine(actualCoroutine);

            actualCoroutine = StartCoroutine(LerpMaterial(true));
        }
    }

    public void desactivarIluminacion()
    {
        if (actualCoroutine != null)
            StopCoroutine(actualCoroutine);

        actualCoroutine = StartCoroutine(LerpMaterial(false)); 
    }

    IEnumerator LerpMaterial(bool activar)
    {
        t = 0f;
        float elapsedTime = 0f;

        float startLight = lightSword.intensity;
        float targetLight = activar ? maxLight : 0f;

        Color startColor = currentMaterial.color;
        Color targetColor = activar ? endMaterial.color : initialMaterial.color;

        while (elapsedTime < lerpDuration)
        {
            t = elapsedTime / lerpDuration;
            lightSword.intensity = Mathf.Lerp(startLight, targetLight, t);

            // Interpolación del color del material
            currentMaterial.color = Color.Lerp(startColor, targetColor, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de establecer los valores finales correctos
        lightSword.intensity = activar ? maxLight : 0f;
        currentMaterial.color = activar ? endMaterial.color : initialMaterial.color;
    }



    public bool actualbool, previusbool;
    private void Update()
    {
        if(actualbool != previusbool)
        {
            if (actualCoroutine != null)
                StopCoroutine(actualCoroutine);

            actualCoroutine = StartCoroutine(LerpMaterial(actualbool));
            previusbool = actualbool;
        }
    }
}
