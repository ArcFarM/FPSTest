using UnityEngine;
using UnityEngine.InputSystem;

namespace FPSSample {
    public class CharMoving : MonoBehaviour {
        #region Variables
        //입력받을 이동 벡터
        Vector2 inputVector;
        CharacterController charCtrl;
        //속력과 방향
        public float moveVelocity = 5f;
        Vector3 dirVector;
        //점프
        [SerializeField] float jumpHeight = 1f;
        //중력
        float gravity = -9.81f;
        [SerializeField] float gravityVelocity;
        //지면 감지
        public Transform checkGround;
        [SerializeField] float checkRadius = 0.2f;
        [SerializeField] LayerMask groundLayer;
        #endregion

        #region Unity Event Methods
        void Start() {
            inputVector = Vector2.zero;
            dirVector = Vector3.zero;
            charCtrl = GetComponent<CharacterController>();
        }

        void Update() {
            //지면이 아니라면 중력 작용
            if (!CheckGround()) {
                gravityVelocity += gravity * Time.deltaTime;
                charCtrl.Move(Vector3.up * gravityVelocity * Time.deltaTime);
            }
            //로컬 축으로 이동
            dirVector = transform.right * inputVector.x + transform.forward * inputVector.y;
            charCtrl.Move(dirVector * moveVelocity * Time.deltaTime);
        }
        #endregion
        #region Custom Methods
        public void OnMove(InputAction.CallbackContext context) {
            inputVector = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context) {
            //지면에 있을 때만 점프 가능
            if(context.started && CheckGround()) {
                //점프 높이 계산
                gravityVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            
        }
        //지면 점검
        bool CheckGround() {
            return Physics.CheckSphere(checkGround.position, checkRadius, groundLayer);
        }
    #endregion
}
}
