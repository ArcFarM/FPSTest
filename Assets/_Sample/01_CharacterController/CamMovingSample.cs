using UnityEngine;
using UnityEngine.InputSystem;

namespace FPSSample {

    public class CamMovingSample : MonoBehaviour {
        #region Variables
        //회전시킬 카메라
        [SerializeField] Transform camTransform;
        //회전 감도와 회전값
        [SerializeField] float sensitivity = 100f;
        float rotateX = 0;
        //마우스 입력값
        Vector2 mouseInput;

        
        #endregion

        #region Unity Event Methods
        void Update() {
            //마우스 이동 감지
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            mouseY = Mathf.Clamp(mouseY, -90f, 40f); //마우스 Y축 회전 제한
            //카메라 회전
            transform.Rotate(Vector3.left* mouseX);
            transform.Rotate(Vector3.up* mouseY);
        }

        #endregion
        #region Custom Methods
        public void OnMouseMove(InputAction.CallbackContext context) {
            //마우스 이동 감지
            mouseInput = context.ReadValue<Vector2>();
            //회전값 계산
            rotateX -= mouseInput.y * sensitivity * Time.deltaTime;
            rotateX = Mathf.Clamp(rotateX, -90f, 40f); //회전값 제한
            camTransform.localRotation = Quaternion.Euler(rotateX, 0, 0);
        }
        #endregion
    }
}

