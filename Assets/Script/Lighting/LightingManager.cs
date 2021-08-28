using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{

    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset preset;
    [SerializeField, Range(0, 24)] private float timeOfDay;

    [SerializeField] private float speed;

    private bool newDay = false;

    private int actualDay;
    private enum daysOfWeek {Monday, Tuesday, Wedesnay, Thuersday, Friday}

    void Update() 
    {
        if (preset == null) return;
        Debug.Log("actualDay:" + actualDay);
        if(Application.isPlaying)
        {
            timeOfDay += Time.deltaTime * speed;
            timeOfDay %= 24;
            Debug.Log("timeOfDay:" + (int) timeOfDay);
            UpdateLighting(timeOfDay / 24f);
            if(timeOfDay >= 23 && newDay == false){
                actualDay++;
                actualDay %= 7;
                newDay = true;
            } else if (timeOfDay <= 1) {
                newDay = false;
            }
        }
    }

    private void UpdateLighting(float TimePercent)
    {
        RenderSettings.ambientLight = preset.ambientColor.Evaluate(TimePercent);
        if (directionalLight != null)
        {
            directionalLight.color = preset.directionalColor.Evaluate(TimePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((TimePercent * 360f) - 90f, 170f, 0));
        }
    }


    private void OnValidate()
    {
        if (directionalLight != null) return;

        if(RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else 
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
    
}
