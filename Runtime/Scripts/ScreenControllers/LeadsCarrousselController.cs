using System.Collections.Generic;
using System.Linq;
using TMPro;
using UI.Tables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeadsCarrousselController : MonoBehaviour
{
    [Header("Layout references")]
    [SerializeField] private TableLayout tableLayoutFields;
    [SerializeField] private TableLayout tableLayoutSteps;

    [Space(10)]
    [Header("Scene/Screen to load after")]
    [Tooltip("Set next screen to be load after form is completed. If 'nextScreen' is null, it will attempt to load the nextScene.")]
    [SerializeField] private GameObject nextScreen;
    [Tooltip("Set name of scene to be load if nextScreen is not seted.")]
    [SerializeField] private string nextScene;

    [Space(10)]
    [Header("Buttons references")]
    [SerializeField] private Button buttonPrev;
    [SerializeField] private Button buttonNext;
    [SerializeField] private Toggle toggleLGPD;

    [Space(10)]
    [Tooltip("Keyboard animator reference")]
    [SerializeField] private Animator keyAnimator;

    [Space(10)]
    [Header("Admin references")]
    [SerializeField] LeadsAdmin leadsAdmin;
    [SerializeField] GameObject ativacaoError;

    //Private only
    private List<TableCell> cellFields;
    private int actualField;

    private List<TMP_InputField> inputFields;
    private List<TableCell> cellSteps;

    // static references
    public static LeadsFormSettings leadsFormSettings;
    public static SettingsSerializer settingsSerializer;
    public static bool isAwaking = true;

    private void OnEnable()
    {
        isAwaking = false;
        ResetInputFields();
        CleanInputs();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            //Exibe tela de Admin
            if (Input.GetKeyDown(KeyCode.L))
            {
                leadsAdmin.gameObject.SetActive(true);
            }
        }
    }

    private void Start()
    {
        //Chega se existe uma registro de "Ativacao atual" no BD
        Ativacao ativacao = AtivacaoManager.GetActual();
        if (ativacao == null)
        {
            //se não, mostra alerta na tela
            ativacaoError.SetActive(true);
        }
    }

    void Awake()
    {
        isAwaking = true;

        settingsSerializer = ScriptableObject.CreateInstance<SettingsSerializer>();
        leadsFormSettings = settingsSerializer.Deserialize() ?? (new());

        leadsAdmin.LoadToggleStatus(leadsFormSettings.GetLeadsAdminToggles());
        SetVisibilityCellFields(leadsFormSettings.GetLeadsAdminToggles());
    }

    public static void SerializeData()
    {
        settingsSerializer.Serialize(leadsFormSettings);
    }

    //carrega exibe/esconde os campos da form de leads apos deserialização do arquivo de settings
    public void SetVisibilityCellFields(bool[] visibilityStatus)
    {
        cellFields = tableLayoutFields.GetComponentsInChildren<TableCell>(true).ToList();
        for (int i = 0; i < cellFields.Count && visibilityStatus.Length >= cellFields.Count; i++)
        {
            cellFields[i].tag = visibilityStatus[i] ? "ShowCell" : "HideCell";
        }
    }

    public void ResetInputFields()
    {
        //cria lista de todos so campos definidos na interface
        cellFields = tableLayoutFields.GetComponentsInChildren<TableCell>(true).ToList();

        //inicialemente oculta todos os campos 
        foreach (var item in cellFields)
            item.gameObject.SetActive(false);

        //depois cria lista apenas com os campos habilitados conforme os settings carregados
        cellFields = GetComponentsInChildren<TableCell>(true).ToList().FindAll(item => item.CompareTag("ShowCell"));

        //se nenhum campo do forma de lead esta habilitado no settings, esconde o form de leads e vai para proxima tela ou cena
        if (cellFields.Count == 0)
        {
            GotoNextScreen();
            return;
        }

        //exibe o promeiro campo
        actualField = 0;
        cellFields[actualField].gameObject.SetActive(true);

        //habilita as bolinhas de steps no topo da pagina apenas para mais do que 1 campo
        if (cellFields.Count > 1)
        {
            tableLayoutSteps.gameObject.SetActive(true);
            cellSteps = tableLayoutSteps.GetComponentsInChildren<TableCell>(true).ToList();
            foreach (var cell in cellSteps)
                cell.gameObject.SetActive(false);
            for (int i = 0; i < cellFields.Count; i++)
            {
                cellSteps[i].gameObject.SetActive(true);
                cellSteps[i].gameObject.GetComponentsInChildren<Image>()[2].color = Color.white;
            }
            cellSteps[0].gameObject.GetComponentsInChildren<Image>()[2].color = Color.blue;
        }
        else //se nenhuma, escode tudo
            tableLayoutSteps.gameObject.SetActive(false);

        //cria lista dos imputFields 
        inputFields = tableLayoutFields.GetComponentsInChildren<TMP_InputField>(true).ToList();

        //reseta e esconde o toggle da LGPD
        toggleLGPD.isOn = false;
        toggleLGPD.gameObject.SetActive(false);
        //desabilita o continuar até a proxima validação de campo
        buttonNext.interactable = false;
    }

    //percorre todos os Validators dos campos que ja tiveram input para habilitar ou não o botão next
    public void ValidateFields()
    {
        if (actualField > cellFields.Count - 1)
            return;
        buttonNext.interactable = false;
        FieldsValidator fieldValidator = cellFields[actualField].GetComponentInChildren<FieldsValidator>();
        if (fieldValidator.GetIsFieldValid())
        {
            if (actualField < cellFields.Count - 1)
            {
                buttonNext.interactable = true;
            }
            //se no ultimo campo, exibe o toggle LGPD
            else if (actualField == cellFields.Count - 1)
            {
                toggleLGPD.gameObject.SetActive(true);
            }

            if (toggleLGPD.isOn)
                buttonNext.interactable = true;
        }

    }


    public void GotoNextField()
    {
        buttonNext.interactable = false;
        GetComponentInChildren<TableLayout>().AutomaticallyRemoveEmptyColumns = false;
        GetComponentInChildren<TableLayout>().AutomaticallyAddColumns = false;

        if (cellFields.Count == 0 || actualField >= cellFields.Count - 1)
        {
            CreateLead();
            CleanInputs();
            ResetInputFields();
            //proxima tela ou cena, conforme o caso
            GotoNextScreen();
        }
        else
        {
            //muda a cor da bolinha do step atual para branco antes de ir pra proxima
            cellSteps[actualField].gameObject.GetComponentsInChildren<Image>()[2].color = Color.white;
            //esconde campo atual antes de ir pro próximo
            cellFields[actualField].gameObject.SetActive(false);
            //passa para proximo campo 
            actualField++;
            if (actualField < cellFields.Count)
            {
                //habilita o campo atual
                cellFields[actualField].gameObject.SetActive(true);
                //seta o foco no campo atual
                cellFields[actualField].GetComponentInChildren<TMP_InputField>(true).Select();
                //exibe o teclado pela animação
                if (keyAnimator.gameObject.activeSelf)
                    keyAnimator.Play("PopUp");

                cellSteps[actualField].gameObject.GetComponentsInChildren<Image>()[2].color = Color.blue;

                //se esta voltando a um campo ja inputado, revalida
                if (cellFields[actualField].GetComponentInChildren<TMP_InputField>().text != string.Empty)
                    ValidateFields();
            }
        }
        //habilita o botão Voltar se não estiver no primeiro campo
        buttonPrev.interactable = actualField > 0;
        //forca o redraw das tables
        tableLayoutFields.CalculateLayoutInputHorizontal();
        tableLayoutFields.CalculateLayoutInputVertical();
        tableLayoutSteps.CalculateLayoutInputHorizontal();
        tableLayoutSteps.CalculateLayoutInputVertical();
    }

    public void GotoPreviousField()
    {
        //esconde teclado
        if (keyAnimator.gameObject.activeSelf)
            keyAnimator.Play("PopDown");

        //reseta cor e esconde o campo atual (aqui actualField tem o valor do campo atual)
        cellFields[actualField].gameObject.SetActive(false);
        //muda a cor da bolinha do step atual para branco
        cellSteps[actualField].gameObject.GetComponentsInChildren<Image>()[2].color = Color.white;
        //volta um campo e ativa (atual--)
        actualField--;
        cellFields[actualField].gameObject.SetActive(true);
        //muda a cor da bolinha do step atual para azul
        cellSteps[actualField].gameObject.GetComponentsInChildren<Image>()[2].color = Color.blue;

        //habilita o botão Voltar se não estiver no primeiro campo
        buttonPrev.interactable = actualField > 0;

        //esconde toggle LGPD e reseta para falso
        toggleLGPD.isOn = false;
        toggleLGPD.gameObject.SetActive(false);

        //executa e testa os validadores novamente
        ValidateFields();
    }

    //vai para próxima tela ou proxima cena, conforme o caso
    private void GotoNextScreen()
    {
        if (nextScreen != null)
            nextScreen.SetActive(true);
        else
            SceneManager.LoadScene(nextScene);
        gameObject.SetActive(false);
    }

    //Insere ou atualiza um registro de Lead
    private void CreateLead()
    {
        //coleta todos so campos de input baseado em seus nomes no editor
        string cpf, email, nome, fone, dtNasc;

        nome = GetInputTextByFildName("Input nome");
        email = GetInputTextByFildName("Input email");
        fone = GetInputTextByFildName("Input fone");
        cpf = GetInputTextByFildName("Input CPF");
        dtNasc = GetInputTextByFildName("Input dt nascimento");
        //se o campo de CPF não estiver habilitado, inere cpf padrão para gerar um registro novo 
        if (cpf == string.Empty)
            cpf = "000.000.000-00";

        //insere na tabela de leads
        LeadManager.CreateOrUpdate(cpf, nome, fone, email, dtNasc);
    }

    //Limpa todos os campos de input para deixar o formulário pronto para novo uso
    public void CleanInputs()
    {
        if (inputFields == null || inputFields.Count == 0)
            inputFields = tableLayoutFields.GetComponentsInChildren<TMP_InputField>(true).ToList();

        foreach (TMP_InputField input in inputFields)
        {
            input.text = string.Empty;
        }

        Toggle[] toggles = tableLayoutFields.GetComponentsInChildren<Toggle>(true);
        foreach (Toggle toggle in toggles)
        {
            toggle.isOn = false;
        }
    }

    private string GetInputTextByFildName(string fildName)
    {
        foreach (var item in inputFields)
        {
            if (item.name.ToLower() == fildName.ToLower())
                return item.text;
        }
        return string.Empty;
    }

}
