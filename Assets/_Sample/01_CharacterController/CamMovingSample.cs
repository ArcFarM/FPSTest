using UnityEngine;
using UnityEngine.InputSystem;

namespace FPSSample {

    public class CamMovingSample : MonoBehaviour {
        #region Variables
        //ȸ����ų ī�޶�
        [SerializeField] Transform camTransform;
        //ȸ�� ������ ȸ����
        [SerializeField] float sensitivity = 100f;
        float rotateX = 0;
        //���콺 �Է°�
        Vector2 mouseInput;

        
        #endregion

        #region Unity Event Methods
        void Update() {
            //���콺 �̵� ����
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            mouseY = Mathf.Clamp(mouseY, -90f, 40f); //���콺 Y�� ȸ�� ����
            //ī�޶� ȸ��
            transform.Rotate(Vector3.left* mouseX);
            transform.Rotate(Vector3.up* mouseY);
        }

        #endregion
        #region Custom Methods
        public void OnMouseMove(InputAction.CallbackContext context) {
            //���콺 �̵� ����
            mouseInput = context.ReadValue<Vector2>();
            //ȸ���� ���
            rotateX -= mouseInput.y * sensitivity * Time.deltaTime;
            rotateX = Mathf.Clamp(rotateX, -90f, 40f); //ȸ���� ����
            camTransform.localRotation = Quaternion.Euler(rotateX, 0, 0);
        }
        #endregion
    }
}

