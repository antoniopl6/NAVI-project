using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour, Interactable
{
    [SerializeField] string sceneName;
    [SerializeField] public Vector2 newPosition;
    // Start is called before the first frame update
    public void Interact()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

    }

    public Vector2 getNewPos(){
        return newPosition;
    }

    public string getScene(){
        return sceneName;
    }
}
