using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Dialog
{
    [System.Serializable]
    //Par de frases choice y un int que indica en que linea de texto va
    public class choice_t
    {
        [SerializeField] public List<string> choicesLine;
        [SerializeField] public int numLine;
    }

    [SerializeField] List<string> lines;
    [SerializeField] List<bool> isMainCharTalking;
    [SerializeField] List<choice_t> choices;

    public List<string> Lines {
        get { return lines; }
    }

    public List<bool> IsMainChar {
        get { return isMainCharTalking; }
    }

    public List<choice_t> Choices {
        get { return choices; }
    }

    //Retorna -1 si no hay choice en esta frase
    public int isChoice(int indexLine) {
        int loop = 0;
        foreach (var choice in choices)
        {
            if(choice.numLine == indexLine){
                return loop;
            }
            loop++;
        }
        return -1;
    }
}
