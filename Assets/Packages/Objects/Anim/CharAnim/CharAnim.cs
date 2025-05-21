using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharAnim : MonoBehaviour {
    private PlayerInput playerInput;
    Animator animator;
    public float velocity = 0;
    public float accel = 5f;
    //코루틴 중복 실행 방지
    bool onceFlag = false;

    void Awake() {
        //InputSystem 사용
        playerInput = GetComponent<PlayerInput>();
        playerInput.onActionTriggered += OnActionTriggered;
        //Animator 사용
        animator = GetComponent<Animator>();
    }

    void OnDestroy() {
        playerInput.onActionTriggered -= OnActionTriggered;
    }

    //초당 가속도 accel 만큼 가속
    IEnumerator SetVelocity(float num) {
        while(Mathf.Abs(velocity - num) > 0.001f) {
            velocity = (velocity < num ? 
                velocity + accel * Time.deltaTime :
                velocity - accel * Time.deltaTime);
            animator.SetFloat("Velocity", velocity);    
            yield return null;
        }
        velocity = num; // 목표 속도 설정
        onceFlag = false; // 가속이 끝나면 플래그 초기화
    }

    private void OnActionTriggered(InputAction.CallbackContext context) {
        // 이동
        if (context.action.name == "Move") {
            if (context.performed) {
                Vector2 moveInput = context.ReadValue<Vector2>();
                if (moveInput != Vector2.zero) {
                    if(!onceFlag) {
                        onceFlag = true;
                        StartCoroutine(SetVelocity(4));
                    }
                    Debug.Log("현재 상태는 이동");
                    //이동 애니메이션 재생
                    animator.SetBool("Walk", true);
                }
            } else if(context.canceled){
                Debug.Log("현재 상태는 대기");
                StartCoroutine(SetVelocity(0));
                //대기 애니메이션 재생
                animator.SetBool("Walk", false);
            }
        }

        // 점프
        if (context.action.name == "Jump" && context.performed) {
            Debug.Log("현재 상태는 점프");
                //점프 애니메이션 재생
                animator.SetBool("Jump", true);
        }

        // 달리기 (Shift 누를 때만 true)
        if (context.action.name == "Sprint") {
            if (context.started) {
                if (!onceFlag) {
                    onceFlag = true;
                    StartCoroutine(SetVelocity(8));
                }
                Debug.Log("현재 상태는 달리기");
                //달리기 애니메이션 재생
                animator.SetBool("Run", true);
            } else if (context.canceled) {
                StartCoroutine(SetVelocity(0)); 
                Debug.Log("달리기 중지");
                //달리기 애니메이션 정지  
                animator.SetBool("Run",false);
            }
        }
    }
}
