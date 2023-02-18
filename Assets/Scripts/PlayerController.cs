using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private bool isRunning;
    private Vector2 input;

    public LayerMask solidObjectsLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
             
            if(input != Vector2.zero){
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
                if (!hasCollision(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }
        
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    private bool hasCollision( Vector3 targetPos){
        if(Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null){
            return true;
        }
        return false;
    }
}
