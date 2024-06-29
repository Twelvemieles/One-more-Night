using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class Dialog
{
    public Sprite characterSprite;
    public Sprite backgroundSprite;
    [TextAreaAttribute]
    public string dialog;
    public string sfxID;
    public float dialogDuration;
    public float dialogDelay;
    public string optionText1;
    public string dialogIDOption1;
    public string optionText2;
    public string dialogIDOption2;
    public string optionText3;
    public string dialogIDOption3;
    public PlayableAsset timelineAnimation;
    public bool isAutomaticNextDialog;
}
[Serializable]
public class DialogConfig
{
    public string dialogID;
    public string characterName;
    public string dialogBackgroundMusicID;
    public List<Dialog> dialogs;
}
[CreateAssetMenu(fileName = "DialogConfig", menuName = "ScriptableObjects/DialogConfigScriptableObject", order = 2)]
public class DialogConfigScriptableObject : ScriptableObject
{
    public List<DialogConfig> Configs;
}
