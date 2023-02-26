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
            


    }

    void Interact()
    {
        var facingDir = new Vector3(myAnim.GetFloat("lastmovex"), myAnim.GetFloat("lastmovey"));
        var interactPos = transform.position + facingDir;
        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        
        if(collider != null)
        {
            collider.GetComponent<InteractableNPC>()?.Interact();
        }
    }
}
