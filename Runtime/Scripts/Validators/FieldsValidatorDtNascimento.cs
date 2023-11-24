using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class FieldsValidatorDtNascimento : FieldsValidator
{

    public override void DeleteLetter()
    {
        string data = new string(inputField.text.Where(char.IsDigit).ToArray());

        if (data.Length != 0)
        {
            inputField.text = data.Remove(data.Length - 1, 1);
        }
    }

    protected override void ValidateField(string value)
    {
        //value = new string(value.Where(char.IsDigit).ToArray());
        textStatus.gameObject.SetActive(false);
        if (IsFieldValid(value))
        {
            Debug.Log("Data válida!");
        }
        else
        {
            if (value.Length > 0)
            {
                textStatus.text = "Digite uma data válida";
                textStatus.gameObject.SetActive(true);
                Debug.Log("Data inválida!");
            }
        }
        FormatField(value);
    }

    protected override void FormatField(string value)
    {
        string formattedData = "";
        
        string cleanedData = new string(value.Where(char.IsDigit).ToArray());

        if (cleanedData.Length >= 2)
        {
            formattedData = string.Format("{0}/", cleanedData.Substring(0, 2));
        }
        else
        {
            formattedData = cleanedData;
        }

        if (cleanedData.Length >= 3 && cleanedData.Length < 4)
        {
            formattedData += string.Format("{0}", cleanedData.Substring(2, 1));
        }
        

        if (cleanedData.Length >=4)
        {
            formattedData += string.Format("{0}/", cleanedData.Substring(2, Mathf.Min(cleanedData.Length - 2, 2)));
        }

        if (cleanedData.Length >= 5)
        {
            formattedData += string.Format("{0}", cleanedData.Substring(4, Mathf.Min(cleanedData.Length - 4, 4)));
        }



        inputField.text = formattedData;
    }

    protected override bool IsFieldValid(string data)
    {
      
        // Expressão regular para validar "dd/mm/aaaa"
        string expressaoRegular = @"^(\d{2})/(\d{2})/(\d{4})$";
        Regex regex = new Regex(expressaoRegular);

        if (!regex.IsMatch(data))
            return false;

        // Extrair os componentes da data
        Match match = regex.Match(data);
        int dia = int.Parse(match.Groups[1].Value);
        int mes = int.Parse(match.Groups[2].Value);
        int ano = int.Parse(match.Groups[3].Value);

        if (ano<1900 || ano>= DateTime.Now.Year)
            return false;

        try
        {
            // Tentar criar uma data
            DateTime date = new DateTime(ano, mes, dia);

            // Verificar se a data criada é igual à data de entrada
            return date.Day == dia && date.Month == mes && date.Year == ano;
        }
        catch (Exception)
        {
            return false;
        }

    }


}
