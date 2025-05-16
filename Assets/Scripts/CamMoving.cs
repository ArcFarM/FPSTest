using UnityEngine;
using UnityEngine.InputSystem;

namespace FPSSample {

    public class CamMoving : MonoBehaviour {
        #region Variables
        //회전시킬 카메라
        [SerializeField] Transform camTransform;
        //회전 감도와 회전값
        [SerializeField] float sensitivity = 200f;
        float rotateX = 0;
        //마우스 입력값
        Vector2 mouseInput;

        
        #endregion

        #region Unity Event Methods
        void Update() {
            //플레이어를 회전 시키기
            transform.Rotate(Vector3.up * mouseInput.x * sensitivity * Time.deltaTime);
            //카메라를 회전 시키기
            rotateX -= mouseInput.y * sensitivity * Time.deltaTime;
                //카메라 회전값 제한
            rotateX = Mathf.Clamp(rotateX, -90f, 40f);
                //원활한 이동을 위해 로컬 축으로 회전
            camTransform.localRotation = Quaternion.Euler(rotateX, 0, 0);
        }

        #endregion
        #region Custom Methods
        public void OnMouseMove(InputAction.CallbackContext context) {
            //마우스 입력값을 받아오기
            mouseInput = context.ReadValue<Vector2>();
        }
        #endregion
    }
}

