using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPSSample {
    public class DoorOpen : MonoBehaviour {
        //문 열기 상호작용 대상에 부착
        #region Variables
        float distance;
        [SerializeField] float threshold = 2f;
        //문 열기 메시지를 띄울 UI
        [SerializeField] GameObject actionUI;
        [SerializeField] TextMeshProUGUI actionText;
        //문 열고 닫는 애니메이션
        [SerializeField] Animator doorAnimator;
        //상호작용 가능한 대상 조우시 십자선 활성화
        [SerializeField] GameObject crosshair;
        #endregion

        #region Unity Event Methods
        private void Update() {
            distance = CheckDistance.distance;
        }
        private void OnMouseOver() {
            if(distance <= threshold) {
                ShowActionUI();
            }
            else {
                 HideActionUI();
            }
        }
        private void OnMouseExit() {
            HideActionUI();
        }
        #endregion

        #region Custom Methods
        void ShowActionUI() {
            actionUI.SetActive(true);
            crosshair.SetActive(true);
        }
        void HideActionUI() {
            actionUI.SetActive(false);
            crosshair.SetActive(false);
        }
        public void OnInteract(InputAction.CallbackContext context) {
            //TODO : 열린 문에 다시 상호작용하면 문닫기
            if(context.started) {
                if (distance <= threshold) {
                    //문 열기
                    doorAnimator.SetBool("IsOpen", true);
                    GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
        #endregion
    }
}