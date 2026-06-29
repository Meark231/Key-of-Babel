using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class GlobalLight : MonoBehaviour
{
    private Light globalLight;

    public string LightEventName;
    public void Start()
    {
        globalLight = GetComponent<Light>();
        EventCenter.Instance.AddEventListener(LightEventName, LightOn);
        globalLight.intensity = 0f;
    }

    public void LightOn()
    {
        globalLight.intensity = 30f;
    }

}
