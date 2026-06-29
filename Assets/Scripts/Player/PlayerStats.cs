using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStats : Singleton<PlayerStats>
{
    public int CurrentTense = 3;
    public float Oxygen = 10.0f;
    public float Temperature = 25.0f;
    public bool isInLowOxygenArea = false;
    public bool isInHighOxygenArea = false;
    public bool isInLowTArea = false;
    public bool isInHighTArea = false;
    public float oxygenDrainPerSecond = 0.4f;
    public float oxygenRecoverPerSecond = 0.2f;
    public bool iffirstoxy = true;
    public TMP_Text Otext;
    public TMP_Text Ttext;
    // Update is called once per frame
    void Update()
    {
        if (isInLowOxygenArea)
        {
            if (iffirstoxy == true)
            {
                DialogSystem.Instance.sayDirect("我", "这里氧气系统似乎故障了,我不能待太久");
                iffirstoxy = false;
            }
            Oxygen -= oxygenDrainPerSecond * Time.deltaTime;
        }
        else if (isInHighOxygenArea)
        {
            Oxygen += oxygenRecoverPerSecond * Time.deltaTime;
        }
        else
        {
            if (Oxygen <= 10)
            {
                Oxygen += oxygenRecoverPerSecond * Time.deltaTime;
            }
            else if (Oxygen >= 10)
            {
                Oxygen -= oxygenRecoverPerSecond * Time.deltaTime;
            }
        }
        if (isInLowTArea)
        {
            Temperature -= 1 * Time.deltaTime;
        }
        else if (isInHighTArea)
        {
            Temperature += 1 * Time.deltaTime;
        }
        else
        {
            if (Temperature <= 25)
            {
                Temperature += 1 * Time.deltaTime;
            }
            else if (Temperature >= 25)
            {
                Temperature -= 1 * Time.deltaTime;
            }
        }

        Oxygen = Mathf.Clamp(Oxygen, 0, 20);
        Otext.text = "氧气:" + Oxygen.ToString("F1");
        Ttext.text = "温度:" + Temperature.ToString("F1");
    }
}
