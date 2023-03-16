using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroCinemDC : MonoBehaviour
{

    public TextMeshProUGUI DialogueText; //recuedro en el que se escribira el texto
    public AudioSource botonAudio; //efecto de sonido para el boton
    public AudioSource interfonoAudio; //efecto de sonido para el interfono
    public string[] Sentences; //dialogos
    private int Index = 0; //indice para los dialogos
    public float DialogueSpeed; //velocidad a la que se escribira el dialogo
    public GameObject cuadroDialogo; //diseño de cuadro de dialogo
    public GameObject cerrar; //boton para cerrar la escena

    private bool StartDialogue = true;
    public int nextScene;

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
                { //momento del interfono, asi que no quiero audio del boton
                    botonAudio.Play();
                }
                NextSentence();
            }

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DialogueText.text = "";
            StartDialogue = true;
            cuadroDialogo.SetActive(false);
            //cerrar.SetActive(true);
            SceneManager.LoadScene(nextScene);
        }

    }

    void NextSentence()
    {

        if (Index <= Sentences.Length - 1)
        {
            DialogueText.text = "";
            StartCoroutine(WriteSentence());

            if (Index == 2) { //momento del interfono
                interfonoAudio.Play();
            }

            if (Index == 4)
            { //momento con pausa
                StartCoroutine(waiter());
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
