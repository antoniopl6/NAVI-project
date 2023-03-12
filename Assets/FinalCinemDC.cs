using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalCinemDC : MonoBehaviour
{
    public TextMeshProUGUI DialogueText; //recuedro en el que se escribira el texto
    public AudioSource botonAudio; //efecto de sonido para el boton
    public AudioSource monsterPackAudio; //efecto de sonido para muchos monstruos
    public AudioSource monsterAudio; //efecto de sonido para un monstruo
    public string[] Sentences; //dialogos
    private int Index = 0; //indice para los dialogos
    public float DialogueSpeed; //velocidad a la que se escribira el dialogo
    public GameObject cuadroDialogo; //diseño de cuadro de dialogo
    public GameObject wackground1; //wackground1
    public GameObject wackground2; //wackground2
    public GameObject cerrar; //boton para cerrar la escena

    private bool StartDialogue = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (StartDialogue)
            {
                StartDialogue = false;
            }
            else
            {
                if (Index != 2)
                { //momento en el que salen los monstruos y suena ahi el ruido
                    botonAudio.Play();
                }
                NextSentence();
            }

        }
    }

    void NextSentence()
    {

        if (Index <= Sentences.Length - 1)
        {
            DialogueText.text = "";
            StartCoroutine(WriteSentence());

            if (Index == 0)
            {
                wackground1.SetActive(true);
            }

            if (Index == 2)
            { //momento en el que sale un monstruo
                monsterAudio.Play();
            }

            if(Index == 4)
            { //momento en el que salen los monstruos y suena ahi el ruido
                wackground1.SetActive(false);
                wackground2.SetActive(true);
            }

            if (Index == 5)
            { //momento en el que salen los monstruos y suena ahi el ruido
                monsterPackAudio.Play();
            }

            if (Index == 9)
            {
                wackground2.SetActive(false);
            }

        }
        else
        {
            DialogueText.text = "";
            StartDialogue = true;
            cuadroDialogo.SetActive(false);
            cerrar.SetActive(true);
        }

    }

    IEnumerator WriteSentence()
    {

        foreach (char Character in Sentences[Index].ToCharArray())
        {

            DialogueText.text += Character;
            yield return new WaitForSeconds(DialogueSpeed);

        }
        Index++;

    }

    IEnumerator waiter()
    {

        //Wait for x seconds
        yield return new WaitForSeconds(2);

    }
}
