using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace Nxr.FormLeads
{
    public class FieldsValidatorCpf : FieldsValidator
    {
        [SerializeField] Toggle toggleCpf;
        private bool isBlockCPFToday = true;
        protected override void OnEnable()
        {
            //toggleCpf.isOn = false;
        }

        protected override void Start()
        {
            base.Start();
            toggleCpf.isOn = false;
        }

        protected override void ValidateField(string value)
        {
            textStatus.gameObject.SetActive(false);

            if (IsFieldValid(value))
            {
                Debug.Log("CPF v�lido!");

            }
            else
            {
                if (IsCPFDuplicateToday(value))
                {
                    textStatus.text = "Você já recebeu seu prêmio hoje ;-)";
                    textStatus.gameObject.SetActive(true);
                }
                else if (value.Length == 14)
                {
                    textStatus.text = "Digite um CPF Válido";
                    textStatus.gameObject.SetActive(true);
                }

                Debug.Log("CPF inv�lido!");
            }
            FormatField(value);
        }

        protected override void FormatField(string value)
        {
            // Remove todos os caracteres n�o num�ricos do novo CPF
            value = new string(value.Where(char.IsDigit).ToArray());
            // Garante que o CPF n�o ultrapasse 11 d�gitos
            value = value.Substring(0, Mathf.Min(value.Length, 11));


            // Formata o novo CPF como "000.000.000-00"
            string formattedCPF = "";
            for (int i = 0; i < value.Length; i++)
            {
                formattedCPF += value[i];
                if ((i == 2 && value.Length > 2) || (i == 5 && value.Length > 5))
                {
                    formattedCPF += ".";
                }
                else if (i == 8 && value.Length > 8)
                {
                    formattedCPF += "-";
                }
            }

            // Atualiza o texto do campo com o CPF formatado
            inputField.text = formattedCPF;
        }

        protected override bool IsFieldValid(string value)
        {
            fieldValid = !IsCPFDuplicateToday(value);
            if (!fieldValid)
                return false;

            // Remova quaisquer caracteres n�o num�ricos do CPF
            value = new string(value.Where(char.IsDigit).ToArray());


            // Verifique se o CPF tem 11 d�gitos
            if (value.Length != 11)
            {
                return false;
            }

            // Verifique se todos os d�gitos s�o iguais (CPF inv�lido)
            if (value.Distinct().Count() == 1)
            {
                if (value[0] == '0' && !inputField.interactable) //permite o "000.000.000-00" para o "não possuo cpf"
                    return true;
                return false;
            }

            // C�lculo do primeiro d�gito verificador
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(value[i].ToString()) * (10 - i);
            }
            int firstVerifierDigit = (sum * 10) % 11;
            if (firstVerifierDigit == 10)
            {
                firstVerifierDigit = 0;
            }

            // C�lculo do segundo d�gito verificador
            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(value[i].ToString()) * (11 - i);
            }
            int secondVerifierDigit = (sum * 10) % 11;
            if (secondVerifierDigit == 10)
            {
                secondVerifierDigit = 0;
            }

            // Verifica se os d�gitos verificadores s�o iguais aos �ltimos dois d�gitos do CPF
            fieldValid = firstVerifierDigit == int.Parse(value[9].ToString()) && secondVerifierDigit == int.Parse(value[10].ToString());
            return fieldValid;
        }

        private bool IsCPFDuplicateToday(string value)
        {
            if (!isBlockCPFToday)
                return false;
            bool existsInLeadsPremio = LeadSorteioManager.GetOneToday(value) != null;
            return (existsInLeadsPremio && value != "000.000.000-00");
        }

        public void ToggleCpfOnChange()
        {
            if (!toggleCpf)
                return;

            if (toggleCpf.isOn)
            {
                inputField.interactable = false;
                inputField.text = "000.000.000-00";

            }
            else
            {
                inputField.interactable = true;
                inputField.text = "";
            }

        }

        public void SetBlockCPFToday(Toggle toggle)
        {
            isBlockCPFToday = toggle.isOn;
        }


    }
}