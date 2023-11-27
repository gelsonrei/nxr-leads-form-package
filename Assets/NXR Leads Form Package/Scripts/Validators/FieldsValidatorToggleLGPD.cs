using UnityEngine;
using UnityEngine.UI;

namespace Nxr.FormLeads
{
    public class FieldsValidatorToggleLGPD : FieldsValidator
    {

        [SerializeField] Toggle toggleLGPD;
        [SerializeField] Button buttonLGPD;

        protected override void Start()
        {
            base.Start();
            toggleLGPD.onValueChanged.AddListener(OnTogleLGPDChaged);
            buttonLGPD.GetComponent<Image>().color = invalidColor;
        }

        private void OnTogleLGPDChaged(bool value)
        {

            if (toggleLGPD.isOn)
            {
                buttonLGPD.GetComponent<Image>().color = validColor;
            }
            else
            {
                buttonLGPD.GetComponent<Image>().color = invalidColor;
            }
            ValidateField("");
        }

        protected override void ValidateField(string value)
        {

            IsFieldValid(value);

            FormatField(value);
        }

        protected override void FormatField(string value)
        {

        }

        protected override bool IsFieldValid(string value)
        {
            if (!toggleLGPD.isOn)
            {

                Debug.Log("Você precisa aceitar o stermos de privacidade!");
                buttonLGPD.GetComponent<Image>().color = invalidColor;
            }

            return toggleLGPD.isOn;
        }


    }
}