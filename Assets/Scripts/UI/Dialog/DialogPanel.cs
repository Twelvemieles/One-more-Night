using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DialogPanel : MonoBehaviour
{
    [SerializeField] private DialogConfigScriptableObject dialogConfig;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] private GameObject introSkipButton;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject Arrow;
    [SerializeField] private Image characterImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private string dialogClickSFXID;
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private List<DialogOptionButton> dialogOptionButtons;
    [SerializeField] private TextMeshProUGUI lockedPanelText;
    [SerializeField] private AudioSource backgroundAudioSource;
    private DialogConfig _actualDialogConfig;
    private int _actualDialogIndex;
    private Action _OnFinishDialog;
    private bool isDialogFinished;
    private bool _hasOptions;
    private string _lockedPanelString;
    public void Start()
    {
        panel.SetActive(false);
        blackScreen.SetActive(false);
        DisableDialogPanel();
        DisableCharacterImage();
        DisableBackgroundSprite();
        _lockedPanelString = lockedPanelText.text;
    }
    public void Update()
    {
        if (!panel.gameObject.activeInHierarchy) return;
        if(Input.anyKeyDown)
        {
            OnDialogClick();
        }
    }
    public void Init(string dialogID,Action OnFinishDialog)
    {
        blackScreen.SetActive(true);
        panel.SetActive(true);
        _actualDialogConfig = GetDialogConfigByID(dialogID);
        _actualDialogIndex = 0;
        SetCharacterName(_actualDialogConfig.characterName);
        PlayBackgroundSong(_actualDialogConfig.dialogBackgroundMusicID);
        _OnFinishDialog = OnFinishDialog;
        StartCoroutine(DoShowNextDialog());
    }
    private DialogConfig GetDialogConfigByID(string dialogID)
    {
        return dialogConfig.Configs.Find(x => x.dialogID == dialogID);
    }
    private IEnumerator DoShowNextDialog()
    {
        if (_actualDialogIndex < _actualDialogConfig.dialogs.Count)
        {
            DisableArrow();
            isDialogFinished = false;
            yield return new WaitForSeconds(GetActualDialog().dialogDelay);
            ShowNextDialog();
            yield return new WaitForSeconds(GetActualDialog().dialogDuration);
            isDialogFinished = true;
            if (string.IsNullOrEmpty(GetActualDialog().optionText1))
            {
                EnableArrow();
            }
            if (GetActualDialog().isAutomaticNextDialog)
            {
                StartCoroutine(DoShowNextDialog());
            }
            _actualDialogIndex++;
        }
        else
        {
            FinishDialog();
        }
    }
    private void ShowNextDialog()
    {
        Sprite backgroundSprite = GetActualDialog().backgroundSprite;
        if (backgroundSprite != null)
        {
            EnableBackgroundSprite();
            SetBackgroundSprite(backgroundSprite);
        }
        else
        {
            DisableBackgroundSprite();
        }
        string dialogText = GetActualDialog().dialog;
        if(!string.IsNullOrEmpty(dialogText) )
        {
            EnableDialogPanel();
            SetDialogText(dialogText);
        }
        else
        {
            DisableDialogPanel();
        }
        Sprite characterSprite = GetActualDialog().characterSprite;
        if (characterSprite != null)
        {
            EnableCharacterImage();
            SetCharacterSprite(characterSprite);
        }
        else
        {
            DisableCharacterImage();
        }

        string sfxID = GetActualDialog().sfxID;
        if (!string.IsNullOrEmpty(sfxID))
        {
            PlaySFX(sfxID);
        }
        PlayableAsset playableAsset = GetActualDialog().timelineAnimation;
        if(playableAsset != null)
        {
            PlayPlayableAsset(playableAsset);
        }
        SetupOptionButtons();
        SetupIntroSkipButton();
        blackScreen.SetActive(false);
    }
    private void PlayPlayableAsset(PlayableAsset playableAsset)
    {
        playableDirector.Play(playableAsset);
    }
    private void SetDialogText(string text)
    {
        dialogText.text = text;
    }
    private void SetCharacterSprite(Sprite sprite)
    {
        characterImage.sprite = sprite;
    }
    private void SetBackgroundSprite(Sprite sprite)
    {
        backgroundImage.sprite = sprite;
    }
    private void SetCharacterName(string text)
    {
        nameText.text = text;
    }
    private void PlaySFX(string sfxID)
    {
        GameManager.inst.AudioManager.PlaySFX(sfxID);
    }
    private void PlayBackgroundSong(string songID)
    {
        if(!string.IsNullOrEmpty(songID))
        {
            backgroundAudioSource.clip = GameManager.inst.AudioManager.GetAudioUsingID(songID);
            backgroundAudioSource.Play();
        }
    }
    public void OnDialogClick()
    {
        if(isDialogFinished && !_hasOptions)
        {
            PlaySFX(dialogClickSFXID);
            StartCoroutine(DoShowNextDialog());
        }
    }
    public void OnOptionClick(int option,bool isLockedButton,bool isVIPOption)
    {
            PlaySFX(dialogClickSFXID);
            PlayerPrefs.SetInt(_actualDialogConfig.dialogID, 1);
            switch (option)
            {
                case 0:
                    string optionDialogID = GetDialogConfigByIndex(_actualDialogIndex - 1).dialogIDOption1;
                    TriggerEvent(optionDialogID);
                    break;
                case 1:
                    string optionDialogID2 = GetDialogConfigByIndex(_actualDialogIndex - 1).dialogIDOption2;
                    TriggerEvent(optionDialogID2);
                    break;
                case 2:
                    string optionDialogID3 = GetDialogConfigByIndex(_actualDialogIndex - 1).dialogIDOption3;
                    TriggerEvent(optionDialogID3);
                    break;
            }
        
    }
    private void ShowLockedPanel(int levelToUnlock)
    {
        lockedPanelText.text = string.Format(_lockedPanelString, levelToUnlock);
        lockedPanelText.GetComponent<Animator>().Play("Show");
    }
    private void TriggerEvent(string optionDialogID)
    {
        if (string.IsNullOrEmpty(optionDialogID))
        {
            FinishDialog();
        }
        else if (Regex.Match(optionDialogID, "<(.*?)>").Success)
        {
            string methodName = Regex.Match(optionDialogID, "<(.*?)>").Value.Trim(new Char[] { '<', '>'});
            Invoke(methodName, 0.01f);
        }
        else if (Regex.Match(optionDialogID, "#(.*?)#").Success)
        {
            string urlLink = Regex.Match(optionDialogID, "#(.*?)#").Value.Trim(new Char[] { '#', '#' });
            GoToLink(urlLink);
        }
        else
        {
            Init(optionDialogID, _OnFinishDialog);
        }
    }
    public void GoToLink(string link)
    {
        if (!string.IsNullOrEmpty(link))
        {
            Application.OpenURL(link);
        }
    }
    private void FinishDialog()
    {
        PlayerPrefs.SetInt(_actualDialogConfig.dialogID, 1);
        Clear();
        _OnFinishDialog?.Invoke();
    }
    private void Clear()
    {
        _actualDialogIndex = 0;
        _actualDialogConfig = null;
        panel.SetActive(false);
        SetCharacterSprite( null);
        SetCharacterName( "");
        SetDialogText("");
        DisableDialogPanel();
        DisableCharacterImage();
        DisableBackgroundSprite();
        DisablePanelButtons();
        backgroundAudioSource.Stop();
        _hasOptions = false;
    }
    private void EnableArrow()
    {
        Arrow.SetActive(true);
    }
    private void DisableArrow()
    {
        Arrow.SetActive(false);
    }
    private void EnableDialogPanel()
    {
        dialogPanel.SetActive(true);
    }
    private void DisableDialogPanel()
    {
        dialogPanel.SetActive(false);
    }
    private void EnableCharacterImage()
    {
        characterImage.gameObject.SetActive(true);
    }
    private void DisableCharacterImage()
    {
        characterImage.gameObject.SetActive(false);
    }
    private void EnableBackgroundSprite()
    {
        backgroundImage.transform.parent.gameObject.SetActive(true);
    }
    private void DisableBackgroundSprite()
    {
        backgroundImage.transform.parent.gameObject.SetActive(false);
    }
    private void SetupOptionButtons()
    {
        DisablePanelButtons();
        foreach(var optionButton in dialogOptionButtons)
        {
            optionButton.Init(GetActualDialog(),this);
        }
        if (!string.IsNullOrEmpty(GetActualDialog().optionText1))
        {
            _hasOptions = true;
            buttonsPanel.SetActive(true);
        }
    }

    public void DisablePanelButtons()
    {
        _hasOptions = false;
        buttonsPanel.SetActive(false);
    }
    public void OnIntroSkipButtonClick()
    {
        StopAllCoroutines();
        PlaySFX(dialogClickSFXID); 
        _actualDialogIndex = _actualDialogConfig.dialogs.Count - 1;
        StartCoroutine(DoShowNextDialog());
    }
    private void SetupIntroSkipButton()
    {
        bool isLastDialog = _actualDialogIndex >= _actualDialogConfig.dialogs.Count - 1;
        introSkipButton.SetActive(!isLastDialog && HasSawTheDialog());

    }
    private bool HasSawTheDialog()
    {
        var value = PlayerPrefs.HasKey(_actualDialogConfig.dialogID);
        return value;
    }
    private bool HasPlayedForFirstTime()
    {
        return PlayerPrefs.HasKey("FirstGame");
    }
    private Dialog GetActualDialog()
    {
        if(_actualDialogConfig.dialogs.Count > _actualDialogIndex)
        {
            return _actualDialogConfig.dialogs[_actualDialogIndex];
        }
        else
        {
            FinishDialog();
            return null;
        }
    }
    private Dialog GetDialogConfigByIndex(int index)
    {
        return _actualDialogConfig.dialogs[index];
    }
}
