using UnityEngine;
using System;

namespace TMPro
{
    /// <summary>
    /// Custom input validator for a date field (DD/MM/YYYY).
    /// </summary>
    [Serializable]
    [CreateAssetMenu(fileName = "InputValidator - Date.asset", menuName = "TextMeshPro/Input Validators/Date", order = 100)]
    public class TMP_DateValidator : TMP_InputValidator
    {
        // Custom text input validation function
        public override char Validate(ref string text, ref int pos, char ch)
        {
            // Aceita apenas dígitos e tamanho máximo de 10 catacteres (DD/MM/YYYY)
            if (text.Length < 10 && (ch >= '0' && ch <= '9') )
            {
                // Adiciona o caractere ao texto
                text += ch;
                pos += 1;

                // Máscara para o formato DD/MM/YYYY
                if (pos == 2 || pos == 5)
                {
                    text += '/';
                    pos += 1;
                }

                return ch;
            }

            return (char)0; // Ignora o caractere inserido
        }
    }
}