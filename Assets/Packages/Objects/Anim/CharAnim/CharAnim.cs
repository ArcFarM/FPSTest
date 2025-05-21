using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharAnim : MonoBehaviour {
    private PlayerInput playerInput;
    Animator animator;
    public float velocity = 0;
    public float accel = 5f;
    //�ڷ�ƾ �ߺ� ���� ����
    bool onceFlag = false;

    void Awake() {
        //InputSystem ���
        playerInput = GetComponent<PlayerInput>();
        playerInput.onActionTriggered += OnActionTriggered;
        //Animator ���
        animator = GetComponent<Animator>();
    }

    void OnDestroy() {
        playerInput.onActionTriggered -= OnActionTriggered;
    }

    //�ʴ� ���ӵ� accel ��ŭ ����
    IEnumerator SetVelocity(float num) {
        while(Mathf.Abs(velocity - num) > 0.001f) {
            velocity = (velocity < num ? 
                velocity + accel * Time.deltaTime :
                velocity - accel * Time.deltaTime);
            animator.SetFloat("Velocity", velocity);    
            yield return null;
        }
        velocity = num; // ��ǥ �ӵ� ����
        onceFlag = false; // ������ ������ �÷��� �ʱ�ȭ
    }

    private void OnActionTriggered(InputAction.CallbackContext context) {
        // �̵�
        if (context.action.name == "Move") {
            if (context.performed) {
                Vector2 moveInput = context.ReadValue<Vector2>();
                if (moveInput != Vector2.zero) {
                    if(!onceFlag) {
                        onceFlag = true;
                        StartCoroutine(SetVelocity(4));
                    }
                    Debug.Log("���� ���´� �̵�");
                    //�̵� �ִϸ��̼� ���
                    animator.SetBool("Walk", true);
                }
            } else if(context.canceled){
                Debug.Log("���� ���´� ���");
                StartCoroutine(SetVelocity(0));
                //��� �ִϸ��̼� ���
                animator.SetBool("Walk", false);
            }
        }

        // ����
        if (context.action.name == "Jump" && context.performed) {
            Debug.Log("���� ���´� ����");
                //���� �ִϸ��̼� ���
                animator.SetBool("Jump", true);
        }

        // �޸��� (Shift ���� ���� true)
        if (context.action.name == "Sprint") {
            if (context.started) {
                if (!onceFlag) {
                    onceFlag = true;
                    StartCoroutine(SetVelocity(8));
                }
                Debug.Log("���� ���´� �޸���");
                //�޸��� �ִϸ��̼� ���
                animator.SetBool("Run", true);
            } else if (context.canceled) {
                StartCoroutine(SetVelocity(0)); 
                Debug.Log("�޸��� ����");
                //�޸��� �ִϸ��̼� ����  
                animator.SetBool("Run",false);
            }
        }
    }
}
