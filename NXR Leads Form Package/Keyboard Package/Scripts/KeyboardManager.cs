using UnityEngine;
using TMPro;

namespace Nxr.FormLeads
{
    public class KeyboardManager : MonoBehaviour
    {
        public static KeyboardManager Instance;
        static TMP_InputField textBox;


        private void Start()
        {
            Instance = this;
            //textBox.text = "";
        }


        public void DeleteLetter()
        {
            if (textBox.GetComponent<FieldsValidator>())
            {
                textBox.GetComponent<FieldsValidator>().DeleteLetter();
            }
            else if (textBox.text.Length != 0)
            {
                textBox.text = textBox.text.Remove(textBox.text.Length - 1, 1);
            }
        }

        public void AddLetter(string letter)
        {
            if (textBox)
                textBox.text = textBox.text + letter;
        }

        public void SubmitWord()
        {
            if (textBox)
                textBox.text = "";
            Debug.Log("Text submitted successfully!");
        }

        public void ResetFields()
        {
            if (textBox)
                textBox.text = "";
        }

        public void SetTextBox(TMP_InputField inputField)
        {
            textBox = inputField;
        }
    }
}