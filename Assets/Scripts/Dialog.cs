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

    [SerializeField] public List<string> lines;
    [SerializeField] public List<bool> isMainCharTalking;
    [SerializeField] public List<choice_t> choices = new List<choice_t>();

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
