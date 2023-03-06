using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] Text optionsText;

    [SerializeField] int lettersForSecond;

    public event Action OnShowDialog;
    public event Action OnHideDialog;

    public static DialogManager Instance { get; private set;}

    Dialog dialog;
    int currentLine = 0;
    int currentOption = 0;
    bool isInOptionState;
    bool isTyping;

    public void HandleUpdate()
    {
        int mod(int x, int m) {
            return (x%m + m)%m;
        }
        if(isInOptionState == true){
            if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
                int ischoice = dialog.isChoice(currentLine);
                List<string> choicesLine = dialog.Choices[ischoice].choicesLine;
                currentOption = mod((currentOption + 1), choicesLine.Count);
                StartCoroutine(TypeOptions(choicesLine, currentOption));
            }
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
                int ischoice = dialog.isChoice(currentLine);
                List<string> choicesLine = dialog.Choices[ischoice].choicesLine;
                currentOption = mod((currentOption - 1), choicesLine.Count);
                StartCoroutine(TypeOptions(choicesLine, currentOption));
            }
            if(Input.GetKeyUp(KeyCode.E)){
                isInOptionState = false;
                optionsText.text = "";
                ++currentLine;
                if(currentLine < dialog.Lines.Count)
                {
                    StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
                }
                else
                {
                    dialogBox.SetActive(false);
                    currentLine = 0;
                    OnHideDialog?.Invoke();
                }
            }
        }
        else if(Input.GetKeyUp(KeyCode.E) && isTyping == false){
            int ischoice = dialog.isChoice(currentLine);

            if (ischoice != -1) {
                StartCoroutine(TypeOptions(dialog.Choices[ischoice].choicesLine, 0));
            } 

            else {
                ++currentLine;
                if(currentLine < dialog.Lines.Count)
                {
                    StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
                }
                else
                {
                    dialogBox.SetActive(false);
                    currentLine = 0;
                    OnHideDialog?.Invoke();
                }
            }
            
        }
    }

    private void Awake() {
        Instance = this;
    }

    public IEnumerator ShowDialog(Dialog dialog)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke();

        this.dialog = dialog;
        dialogBox.SetActive(true);
        optionsText.text = "";
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersForSecond);
        }
        isTyping = false;
    }


    public IEnumerator TypeOptions(List<string> choicesLines, int i)
    {
        yield return new WaitForEndOfFrame();
        optionsText.text = "";
        isInOptionState = true;
        int loop = 0;
        currentOption = i;
        foreach (var choice in choicesLines)
        {
            if(loop == i){
                optionsText.text += "> " + choice + "\n";
            }
            else {
                optionsText.text += choice + "\n";
            } 
            ++loop;
        }
        
    }
}
