using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;


public class CTRL : MonoBehaviour
{
    private PlayerControl playercontrol;

    public float moveSpeed = 5.0f;
    public float turnSpeed = 10.0f;
    public Animator animator;
    public GameObject walkVFX;


    private Vector3 currentVelocity;

    private Vector2 movementInput = Vector2.zero;
    private bool act01, act02, act03, act04 = false;

    private void Start()
    {
        playercontrol = new PlayerControl();

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnAct1(InputAction.CallbackContext context)
    {
        //actAttack = context.ReadValue<bool>();
        act01 = context.action.triggered;
    }

    public void OnAct2(InputAction.CallbackContext context)
    {
        act02 = context.action.triggered;
    }

    public void OnAct3(InputAction.CallbackContext context)
    {
        act03 = context.action.triggered;
    }

    public void OnAct4(InputAction.CallbackContext context)
    {
        act04 = context.action.triggered;
    }





    private void Update()
    {

        Vector3 movement = new Vector3(movementInput.x, 0, movementInput.y).normalized * moveSpeed;
        currentVelocity = movement;

        transform.Translate(currentVelocity * Time.deltaTime, Space.World);

        Vector3 targetDirection = new Vector3(movementInput.x, 0, movementInput.y);
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }


        //animator.SetFloat("Speed", currentVelocity.magnitude);

        //if(currentVelocity.magnitude >= 0.01)
        //{
        //    walkVFX.SetActive(true);
        //}
        //else if (currentVelocity.magnitude < 0.01)
        //{
        //    walkVFX.SetActive(false);
        //}



    }

    float Value = 0;

    private void Action01()
    {
        Debug.Log("Action 01");
    }

    private void Action02()
    {
        Debug.Log("Action 02");
    }

    private void Action03()
    {
        Debug.Log("Action 03");
    }

    public bool IsAnyActionKeyPressed()
    {
        return act01 || act02 || act03 || act04;
    }

    public bool IsAct01Triggered() { Debug.Log("Action 01"); return act01; }
    public bool IsAct02Triggered() { Debug.Log("Action 02"); return act02; }
    public bool IsAct03Triggered() { Debug.Log("Action 03"); return act03; }
    public bool IsAct04Triggered() { Debug.Log("Action 04"); return act04; }

    private IEnumerator MoveForward(Vector3 endPos)
    {
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;

        while (elapsedTime < 0.5f)
        {

            float t = elapsedTime / 0.5f;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
    }

}
