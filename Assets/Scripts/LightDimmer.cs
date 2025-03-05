using UnityEngine;
using System.Collections;

public class LightDimmer : MonoBehaviour
{
    public float dimDuration = 2f;
    public float targetIntensity = 0.1f;
    private Light[] allLights;
    private float[] originalIntensities;
    private float originalAmbientIntensity;

    void Start()
    {
        allLights = FindObjectsOfType<Light>();
        originalIntensities = new float[allLights.Length];

        for (int i = 0; i < allLights.Length; i++)
        {
            originalIntensities[i] = allLights[i].intensity;
        }

        originalAmbientIntensity = RenderSettings.ambientIntensity; // Store original GI intensity

        CauldronUnlocker.OnCauldronUnlocked += DimLights;
    }

    private void DimLights()
    {
        Debug.Log("✨ Dimming all lights and global illumination...");
        StartCoroutine(DimLightsCoroutine());
    }

    private IEnumerator DimLightsCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < dimDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / dimDuration;

            for (int i = 0; i < allLights.Length; i++)
            {
                if (allLights[i] != null)
                {
                    allLights[i].intensity = Mathf.Lerp(originalIntensities[i], targetIntensity, t);
                }
            }

            RenderSettings.ambientIntensity = Mathf.Lerp(originalAmbientIntensity, targetIntensity, t);

            yield return null;
        }

        Debug.Log("✨ Lights and GI dimmed successfully!");
    }

    private void OnDestroy()
    {
        CauldronUnlocker.OnCauldronUnlocked -= DimLights;
    }
}
