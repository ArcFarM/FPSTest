using TMPro;
using UnityEngine;

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
        #endregion

        #region Unity Event Methods
        private void Update() {
            distance = CheckDistance.distance;
            if(actionUI.activeSelf) {
                //E키를 눌렀을 때 문이 열리도록 
            }

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
        }
        void HideActionUI() {
            actionUI.SetActive(false);
        }
        #endregion
    }
}