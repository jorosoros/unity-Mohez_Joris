using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    walk,
    attack,
    interact
}

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public float numberForColl;

    private bool isMoving;

    private Vector2 input;

    private Animator animator;

    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("Attacking", true);
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(.3f);
    }

    private void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        Debug.Log(Globals.previousLocation);


        SetPlayerStartLocation();
    }

    private void SetPlayerStartLocation()
    {
        if (SceneManager.GetActiveScene().name == "World")
        {
            if (Globals.previousLocation == "Room1")
            {
                transform.position = new Vector2(-3.5f, 18);
            }

            else if (Globals.previousLocation == "Room2")
            {
                transform.position = new Vector2(-12.5f, 18);
            }
        }
    }
    public void HandleUpdate()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //Debug.Log("This is input.x: " + input.x);
            //Debug.Log("This is input.y: " + input.y);

            if (Input.GetButtonDown("attack"))
            {
                StartCoroutine(AttackCo());
            }


            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.Z))
            Interact();
    }

    void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        Debug.DrawLine(transform.position, interactPos, Color.red, 1f);

        var collider = Physics2D.OverlapCircle(interactPos, numberForColl, interactableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, numberForColl, solidObjectsLayer | interactableLayer) != null)
        {
            return false;
        }
        return true;
    }

}
