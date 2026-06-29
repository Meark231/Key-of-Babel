using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class LowOxygenArea : MonoBehaviour
{
    public string upevent;
    public string downevent;
    public int oxygenValue = -1;
    void Start()

    {
        EventCenter.Instance.AddEventListener(upevent, add1);
        EventCenter.Instance.AddEventListener(downevent, sub1);

    }
    private void add1()
    {
        oxygenValue += 1;
        oxygenValue = Mathf.Clamp(oxygenValue, -1, 1);
        Debug.Log("已加1 现在的值为" + oxygenValue);
    }
    private void sub1()
    {
        oxygenValue -= 1;
        oxygenValue = Mathf.Clamp(oxygenValue, -1, 1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        PlayerStats status = other.GetComponent<PlayerStats>();

        if (status != null)
        {
            switch (oxygenValue)
            {
                case -1: status.isInLowOxygenArea = true; break;
                case 0: break;
                case 1: status.isInHighOxygenArea = true; break;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        PlayerStats status = other.GetComponent<PlayerStats>();

        if (status != null)
        {
            status.isInLowOxygenArea = false;
            status.isInHighOxygenArea = false;
        }
    }
}