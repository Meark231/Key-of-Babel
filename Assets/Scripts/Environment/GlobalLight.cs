using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class GlobalLight : MonoBehaviour
{
    private Light globalLight;

    public void Start()
    {
        globalLight = GetComponent<Light>();
        EventCenter.Instance.AddEventListener("OpenLocalLight", LightOn);
    }

    public void LightOn()
    {
        if (ProcessSystem.Instance.IfPowerful) globalLight.intensity = 40f;
    }

}
