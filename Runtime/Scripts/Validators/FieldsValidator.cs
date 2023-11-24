using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class FieldsValidator : MonoBehaviour
{
    [SerializeField] protected TMP_InputField inputField;
    [SerializeField] protected GameObject line;
    [SerializeField] protected TMP_Text textStatus;

    protected bool fieldValid = false;

    protected Color inputColor;
    protected Color validColor = new (0.57f, 0.924f, 0.62f, 0.39f);
    protected Color invalidColor = new(0.98f, 0.87f, 0.87f, 1.0f);
    protected Color blankColor = new(1.0f, 1.0f, 1.0f, 1.0f);

    public virtual void DeleteLetter()
    {
        if (inputField == null)
            return;

        string value = new string(inputField.text.Where(char.IsDigit).ToArray());

        if (value.Length != 0)
        {
            inputField.text = value.Remove(value.Length - 1, 1);
        }
    }

    protected virtual void OnEnable()
    {
        if (inputField != null)
        {
            //inputField.text = "";
            //inputColor = inputField.GetComponent<Image>().color;
        }
    }

    protected abstract void ValidateField(string value);


    protected abstract void FormatField(string value);


    protected abstract bool IsFieldValid(string value);

    public bool GetIsFieldValid()
    {
        string value = inputField == null? "" : inputField.text;
        bool isValid = IsFieldValid(value);
        if (line != null)
            line.GetComponent<Image>().color = isValid ? validColor : invalidColor;
        return isValid;
    }

    protected virtual void Start()
    {
        fieldValid = false;
        if (inputField!= null)
        {
            inputField.text = "";
            inputField.onValueChanged.AddListener(ValidateField);
            inputColor = line.GetComponent<Image>().color;
        }
        if (textStatus != null)
            textStatus.gameObject.SetActive(false);
    }


}
