using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AdvanceDialogueSO : ScriptableObject
{
    public DialogueActors[] actors;

    [Tooltip("dibutuhkan jika random dipilih sebagai nama aktor")]
    [Header("Random Actor Info")]
    public string randomActorName;
    public Sprite randomActorPortrait;

    [Header("Dialogue")]
    [TextArea]
    public string[] dialogue;
    
    [Tooltip("Pilihan untuk dialogue")]
    public string[] optionText;

    public AdvanceDialogueSO option0;
    public AdvanceDialogueSO option1;
    public AdvanceDialogueSO option2;
    public AdvanceDialogueSO option3;

}
