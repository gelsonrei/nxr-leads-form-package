using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class FieldsValidatorNome : FieldsValidator
{


    public override void DeleteLetter()
    {
        string value = inputField.text;

        if (value.Length != 0)
        {
            inputField.text = value.Remove(value.Length - 1, 1);
        }
    }

    protected override void ValidateField(string value)
    {
        textStatus.gameObject.SetActive(false);
       
        if (IsFieldValid(value))
        {
            Debug.Log("Nome v�lido!");
        }
        else
        {
            if (value.Length > 0)
            {
                textStatus.text = "Digite um nome válido";
                
                textStatus.gameObject.SetActive(true);
                Debug.Log("Nome inv�lido!");
            }
        }
        FormatField(value);
    }

    protected override void FormatField(string value)
    {
        //sem formatação para email
        string formattedCPF = value;
        // Atualiza o texto do campo com o CPF formatado
        inputField.text = formattedCPF;
    }

    protected override bool IsFieldValid(string value)
    {
        // Padrão de expressão regular para validar o e-mail
        string pattern = @"^[a-zA-Z-' ]+$";

        // Verifica se o e-mail corresponde ao padrão
        fieldValid =  Regex.IsMatch(value, pattern);
        return fieldValid;
    }


}
