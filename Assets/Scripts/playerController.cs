using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public Rigidbody2D myRB;
    public Animator myAnim;
    public LayerMask interactableLayer;
    public LayerMask doorsLayer;
    public LayerMask colectableLayer;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float runFactor;
    [SerializeField]
    private float stealthFactor;
    private bool isRunning;
    private bool isStealth;
    private bool isDealt;
    private Vector2 newPos;
    private string newScene;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
            
            Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                movement.y = 0;
            }
            else
            {
                movement.x = 0;
            }
            myRB.velocity = new Vector2(movement.x, movement.y) * speed;

            //Stealth mode try
            if (Input.GetKeyDown(KeyCode.LeftControl)) {
                isStealth = !isStealth;
                isRunning = false;
            }

            //Run mode try
            else if (Input.GetKeyDown(KeyCode.LeftShift) || isRunning == true) {
                myRB.velocity = myRB.velocity * runFactor;
                isRunning = true;
            }

            //Exit stealth mode
            if (isStealth == true) {
                myRB.velocity = myRB.velocity / stealthFactor;
            }

            myAnim.SetFloat("moveX", myRB.velocity.x);
            myAnim.SetFloat("moveY", myRB.velocity.y);

            if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                myAnim.SetFloat("lastmovex", Input.GetAxisRaw("Horizontal"));
                myAnim.SetFloat("lastmovey", Input.GetAxisRaw("Vertical"));
            }

            if (Input.GetKeyDown(KeyCode.E)){
                Interact();
            }
                
            //Exit running mode
            if (Input.GetKeyUp(KeyCode.LeftShift)) {
                isRunning = false;
            }
        
    }

    void Interact()
    {
        var facingDir = new Vector3(myAnim.GetFloat("lastmovex"), myAnim.GetFloat("lastmovey"));
        var interactPos = transform.position + facingDir;
        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        var colliderDoor = Physics2D.OverlapCircle(interactPos, 0.2f, doorsLayer);
        var colliderColectable = Physics2D.OverlapCircle(interactPos, 0.2f, colectableLayer);
        if(colliderDoor != null)
        {
            colliderDoor.GetComponent<Interactable>()?.Interact();
        }
        if(colliderColectable != null)
        {            
            string name = colliderColectable.GetComponent<ColectableController>()?.nameColectable;
            Dialog dialog = new Dialog();
            string phrase = "Has obtenido " + name;
            dialog.lines = new List<string>();
            dialog.lines.Add(phrase);
            dialog.isMainCharTalking = new List<bool>();
            dialog.isMainCharTalking.Add(false);
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog, ""));
            colliderColectable.GetComponent<Interactable>()?.Interact();
        }
        if(collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }



    



}
