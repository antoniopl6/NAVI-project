using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody2D myRB;
    private Animator myAnim;
    public LayerMask interactableLayer;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float runFactor;
    [SerializeField]
    private float stealthFactor;
    private bool isRunning;
    private bool isStealth;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void HandleUpdate()
    {
        

        myRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;

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
        
        if(collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }
}
