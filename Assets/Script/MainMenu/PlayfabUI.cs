using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayfabUIManager : MonoBehaviour
{
    public static PlayfabUIManager Instance { get; private set; }

    [Header("Panels")]
    public GameObject panelNoInternet;
    public GameObject panelInputName;
    public GameObject panelChangeName;

    [Header("Input Fields")]
    public TMP_InputField inputFieldName;
    public TMP_InputField inputFieldChangeName;

    [Header("Buttons")]
    public Button buttonSave;
    public Button buttonChangeSave;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        buttonSave.interactable = false;
        buttonChangeSave.interactable = false;
    }

    public void ShowNoInternetPanel() => panelNoInternet.SetActive(true);
    public void HideNoInternetPanel() => panelNoInternet.SetActive(false);

    public void ShowInputNamePanel() => panelInputName.SetActive(true);
    public void HideInputNamePanel() => panelInputName.SetActive(false);

    public void OpenChangeNamePanel()
    {
        inputFieldChangeName.text = PlayfabManager.stringNamePlayer;
        panelChangeName.SetActive(true);
        CheckInputChangeField();
    }

    public void CloseChangeNamePanel() => panelChangeName.SetActive(false);

    public void CheckInputNameField()
    {
        buttonSave.interactable = !string.IsNullOrEmpty(inputFieldName.text);
    }

    public void CheckInputChangeField()
    {
        buttonChangeSave.interactable = !string.IsNullOrEmpty(inputFieldChangeName.text);
    }

    public void OnClickSaveName()
    {
        buttonSave.interactable = false;
        PlayfabManager.Instance.UpdateDisplayName(inputFieldName.text, () =>
        {
            HideInputNamePanel();
            PlayfabManager.Instance.SendLeaderboard(0);
        },
        () =>
        {
            buttonSave.interactable = true;
        });
    }

    public void OnClickSaveChangedName()
    {
        if (string.IsNullOrEmpty(inputFieldChangeName.text)) return;

        PlayfabManager.Instance.UpdateDisplayName(inputFieldChangeName.text, () =>
        {
            CloseChangeNamePanel();
        },
        () =>
        {
            buttonChangeSave.interactable = true;
        });
    }
}
