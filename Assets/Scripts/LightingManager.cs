using Unity.Multiplayer.Center.Common;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    // References
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset currentPreset;
    // Variables
    [SerializeField, Range(0, 24)] private float timeOfDay;

    private void Update()
    {
        if (currentPreset == null)
            return;

        if (Application.isPlaying)
        {
            timeOfDay += Time.deltaTime;
            timeOfDay %= 24; // Loop the time of day back to 0 after reaching 24
            UpdateLighting(timeOfDay / 24f);
        }
        else
        {
            UpdateLighting(timeOfDay / 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = currentPreset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = currentPreset.fogColor.Evaluate(timePercent);

        if (directionalLight != null)
        {
            directionalLight.color = currentPreset.directionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }
    
    // Try to find the directional light in the scene if it's not already assigned
    private void OnValidate()
    {
        if (directionalLight != null)
        return;

        // Search for the directional light in the scene
        if (RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        // Search scene for the light that fits the criteria (directional light)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}
