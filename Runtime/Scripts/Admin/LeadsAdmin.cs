using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class LeadsAdmin : MonoBehaviour
{
    List<Toggle> fieldsToggles;

    public void LoadToggleStatus(bool[] booleanArray)
    {

        fieldsToggles = GetComponentsInChildren<Toggle>(true).ToList();

        if (booleanArray != null && booleanArray.Length > 0)
        {
            for (int i = 0; i < fieldsToggles.Count; i++)
            {
                fieldsToggles[i].isOn = booleanArray[i];
            }
        }
    }

    public void SerializeToggleStatus()
    {
        //flag isAwaking evita que entre em recursao no disparo do evento OnChange do toggle
        if (LeadsCarrousselController.isAwaking) return;

        fieldsToggles = GetComponentsInChildren<Toggle>(true).ToList();
        bool[] booleanArray = fieldsToggles.Select(toggle => toggle.isOn).ToArray();
        LeadsCarrousselController.leadsFormSettings.SetLeadsAdminToggles(booleanArray);
        LeadsCarrousselController.leadsFormSettings.SetLeadsAdminToggles(fieldsToggles);
        LeadsCarrousselController.SerializeData();
    }
}
