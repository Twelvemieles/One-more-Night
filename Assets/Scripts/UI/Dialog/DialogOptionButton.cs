using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogOptionButton : MonoBehaviour
{
    [SerializeField] private int optionIndex;
    [SerializeField] private GameObject vipIcon;
    [SerializeField] private GameObject lockedIcon;
    [SerializeField] private TextMeshProUGUI optionText;
    [SerializeField] private Button button;
    private DialogPanel _dialogPanel;
    public void Init(Dialog dialogConfig, DialogPanel dialogPanel)
    {
        _dialogPanel = dialogPanel;
        string buttonText = string.Empty;
        EnableButton(false);
        switch (optionIndex)
        {
            case 0:
                buttonText = dialogConfig.optionText1;
                EnableButton(!string.IsNullOrEmpty(buttonText));
                break;
                case 1:
                buttonText = dialogConfig.optionText2;
                EnableButton(!string.IsNullOrEmpty(buttonText));
                break;
                case 2:
                buttonText = dialogConfig.optionText3;
                EnableButton(!string.IsNullOrEmpty(buttonText));
                break;
        }
        SetDialogText(buttonText);
    }
    private void EnableButton(bool enable)
    {
        button.gameObject.SetActive(enable);
    }
    private void SetDialogText(string text)
    {
        optionText.text = text;
    }
    public void OnClick()
    {
        _dialogPanel.OnOptionClick(optionIndex, lockedIcon.activeInHierarchy,vipIcon.activeInHierarchy);
    }
}
