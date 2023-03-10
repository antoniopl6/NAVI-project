using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {FreeRoam, Dialog}
public class gameController : MonoBehaviour
{

    [SerializeField] playerController IplayerController;

    GameState state;


    // Start is called before the first frame update
    private void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };
        DialogManager.Instance.OnHideDialog += () =>
        {
            if(state == GameState.Dialog)
                state = GameState.FreeRoam;
        };
    
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.FreeRoam)
        {
            IplayerController.HandleUpdate();
        } 
        else if (state == GameState.Dialog)
        {
            IplayerController.myRB.velocity = new Vector2(0,0);
            IplayerController.myAnim.SetFloat("moveX", 0);
            IplayerController.myAnim.SetFloat("moveY", 0);
            DialogManager.Instance.HandleUpdate();
        }
    }
}
