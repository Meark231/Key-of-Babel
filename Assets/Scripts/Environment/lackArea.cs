using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class LowArea : MonoBehaviour
{
    public string upOevent;
    public string downOevent;
    public string upTevent;
    public string downTevent;
    public int oxygenValue = 1;
    public int TValue = -1;
    void Start()

    {
        EventCenter.Instance.AddEventListener(upOevent, add1);
        EventCenter.Instance.AddEventListener(downOevent, sub1);
        EventCenter.Instance.AddEventListener(upTevent, addt1);
        EventCenter.Instance.AddEventListener(downTevent, subt1);

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
     private void addt1()
    {
        TValue += 1;
        TValue = Mathf.Clamp(TValue, -1, 1);
        Debug.Log("已加1 现在的值为" + TValue);
    }
    private void subt1()
    {
       TValue -= 1;
        TValue = Mathf.Clamp(TValue, -1, 1);
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
             switch (TValue)
            {
                case -1: status.isInLowTArea = true; break;
                case 0: break;
                case 1: status.isInHighTArea = true; break;
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
            status.isInLowTArea = false;
            status.isInHighTArea = false;
        }
    }
}