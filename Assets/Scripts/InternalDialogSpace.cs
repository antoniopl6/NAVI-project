using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternalDialogSpace : MonoBehaviour
{
    private bool isActive = true;
    [SerializeField] bool oneTime = true;
    [SerializeField] public Dialog dialog;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            if (isActive) {
                if (oneTime) {
                    isActive = false;
                }
                StartCoroutine(DialogManager.Instance.ShowDialog(dialog, ""));
                
            }
        }

    }
    
}
