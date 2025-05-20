using UnityEngine;
using TMPro;
using Palmmedia.ReportGenerator.Core.CodeAnalysis;
using UnityEngine.InputSystem;

namespace FPSSample {
    public class GetWeapon : MonoBehaviour
    {
        #region Variables
        //대상과 플레이어 사이의 거리
        float distance;
        [SerializeField] float threshold = 2f;
        //상호작용 시 나올 대상
        [SerializeField] GameObject actionUI;
        [SerializeField] TextMeshProUGUI actionText;
        Animator weaponAnimator;
        string actionString = "총 집기";
        [SerializeField] GameObject playerWeapon;
        //강조 화살표
        [SerializeField] GameObject highlightArrow;
        //상호작용 가능한 대상 조우시 십자선 활성화
        [SerializeField] GameObject crosshair;
        //실행되면 한 번만 실행되어야 함
        bool onceFlag = false;
        #endregion

        #region Unity Event Methods
        private void Update() {
            distance = CheckDistance.distance;
        }
        private void OnMouseOver() {
            if (distance <= threshold && !onceFlag) {
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
            //무기 획득
            if (distance <= threshold) {
                if (!onceFlag) {
                    onceFlag = true;
                    actionText.text = actionString;
                    //TODO : 무기 장착 애니메이션 재생
                    playerWeapon.SetActive(true);
                    //다 끝났다면 콜라이더를 꺼서 상호작용 없애기
                    highlightArrow.SetActive(false);
                    this.gameObject.SetActive(false);
                }
            }
        }
        #endregion
    }
}