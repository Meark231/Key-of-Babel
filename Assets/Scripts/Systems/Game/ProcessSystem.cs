using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ProcessSystem : Singleton<ProcessSystem>
{
    public bool IfPowerful = false;
    public void Start()
    {
        EventCenter.Instance.AddEventListener("OpenLocalPower", OpenLocalPower);
        EventCenter.Instance.AddEventListener("CloseLocalPower", CloseLocalPower);
    }
    public void OpenLocalPower()
    {
        IfPowerful = true;

    }
    public void CloseLocalPower()
    {
         IfPowerful = false;
     
    }
}
