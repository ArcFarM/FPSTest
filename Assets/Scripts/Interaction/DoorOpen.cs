using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPSSample {
    public class DoorOpen : InterAction {
        //문 열기 상호작용 대상에 부착
        #region Variables
        [SerializeField]AudioSource door_as;
        #endregion

        #region Unity Event Methods
        private void Start() {
        }
        #endregion

        #region Custom Methods
        public override void OnInteract() {
            //문 열라고 문구 나오게 하기
            actionText.text = "문 열기";
            //TODO : 열린 문에 다시 상호작용하면 문닫기
            animator.SetBool("IsOpen", true);
            GetComponent<BoxCollider>().enabled = false;
            door_as.Play();
        }
        #endregion
    }
}