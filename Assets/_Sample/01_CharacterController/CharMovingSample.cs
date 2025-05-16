using UnityEngine;
using UnityEngine.InputSystem;

namespace FPSSample {
    public class CharMovingSample : MonoBehaviour {
        #region Variables
        //�Է¹��� ����
        Vector2 inputVector;
        CharacterController charCtrl;
        //�ӷ°� ����
        public float velocity = 5f;
        Vector3 dirVector;
        #endregion
        
        #region Unity Event Methods
        void Start() {
            inputVector = Vector2.zero;
            dirVector = Vector3.zero;
            charCtrl = GetComponent<CharacterController>();
        }

        void Update() {
            dirVector = new Vector3(inputVector.x, 0f, inputVector.y);
            charCtrl.Move(dirVector * velocity * Time.deltaTime);
        }

        public void OnMove(InputAction.CallbackContext context) {
            inputVector = context.ReadValue<Vector2>();
        }
        #endregion
    }
}
