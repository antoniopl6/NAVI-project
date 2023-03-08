using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColectableController : MonoBehaviour, Interactable
{
    [SerializeField] public string nameColectable;

    // Start is called before the first frame update
    public void Interact()
    {
        gameObject.SetActive(false);
    }

    
}
