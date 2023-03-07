using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    [SerializeField] string nameNPC;
    // Start is called before the first frame update
    public void Interact()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog, nameNPC));
    }

}
