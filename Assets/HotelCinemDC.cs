using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HotelCinemDC : MonoBehaviour
{

    public TextMeshProUGUI DialogueText; //cuadro de texto de dialogo
    public AudioSource botonSound; //efecto de sonido de boton
    //diferentes sonidos de fondo
    public AudioSource escenaTranquila;
    public AudioSource escenaNoche;
    public AudioSource escenaTension;
    //otros efectos de sonido
    public AudioSource pasos;
    public AudioSource golpes;
    public AudioSource puertaRompiendose;
    public AudioSource grunyidoMonstruo;
    public AudioSource gritoMonstruo;
    public AudioSource saltoPorLaVentana;
    public AudioSource puerta;
    public AudioSource cerrojo;

    public string[] Sentences; //dialogos
    private int Index = 0; //indice
    public float DialogueSpeed; //velocidad de texto
    public GameObject cuadroDialogo; //objeto del dise?o del cuadro de dialogo
    public GameObject cerrar; //objeto del boton de cerrar, para cerrar la cinematica
    //los diferentes wackgrounds
    public GameObject wackgroundNegro;
    public GameObject wackgroundHabitacionDia;
    public GameObject wackgroundHabitacionNoche;
    public GameObject wackgroundPuzzle;

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
                
                if (Index != 1 && Index != 5 && Index != 8 && Index != 13 && Index != 15 && Index != 17 && Index != 21 && Index != 23)
                { //momento de sonido, entonces no quiero sonido de boton
                    botonSound.Play();
                }

                if (Index == 1)
                { //muestra la habitacion
                    wackgroundHabitacionDia.SetActive(true);
                    escenaTranquila.Play();
                }

                if (Index == 5)
                { //muestra puzzle
                    wackgroundHabitacionDia.SetActive(false);
                    wackgroundPuzzle.SetActive(true);
                }

                if (Index == 8)
                { //muestra habitacion
                    wackgroundPuzzle.SetActive(false);
                    wackgroundHabitacionDia.SetActive(true);
                }

                if (Index == 9)
                { //muestra en negro
                    wackgroundHabitacionDia.SetActive(false);
                }

                if (Index == 10)
                { //muestra habitacion noche
                    wackgroundHabitacionNoche.SetActive(true);
                    escenaTranquila.Stop();
                    escenaNoche.Play();
                }

                if (Index == 18)
                { //muestra habitacion noche
                    wackgroundHabitacionNoche.SetActive(true);
                    escenaNoche.Stop();
                    escenaTension.Play();
                }

                if (Index == 23)
                { //muestra en negro
                    wackgroundHabitacionNoche.SetActive(false);
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

            if (Index == 1)
            { //puerta
                puerta.Play();
            }

            if (Index == 5)
            { //cerradura
                cerrojo.Play();
            }

            if (Index == 8)
            { //cerradura
                cerrojo.Play();
            }

            if (Index == 13)
            { //pasos
                pasos.Play();
            }

            if (Index == 15)
            { //grunyido
                pasos.Stop();
                grunyidoMonstruo.Play();
            }

            if (Index == 17)
            { //golpes y grito
                golpes.Play();
                gritoMonstruo.Play();
            }

            if (Index == 21)
            { //grito y puerta rompiendose
                gritoMonstruo.Play();
                puertaRompiendose.Play();
            }

            if (Index == 23)
            { //salto ventana
                saltoPorLaVentana.Play();
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
