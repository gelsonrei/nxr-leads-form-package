using UnityEngine;
using UnityEngine.UI;
namespace Nxr.FormLeads
{
    public class LeadAdminToggleField : MonoBehaviour
    {
        public void UpdateToglleStatus(GameObject objectToToggle)
        {
            //Exibe/esconde os campos da form de leads colocando tags a cada toggle clicado relacionado
            //flag gameManagerIsAwaking evita que entre em recursao no disparo do evento OnChange do toggle
            //quando carregado do arquivo serializado
            if (!LeadsCarrousselController.isAwaking)
                objectToToggle.tag = GetComponent<Toggle>().isOn ? "ShowCell" : "HideCell";
        }
    }
}
