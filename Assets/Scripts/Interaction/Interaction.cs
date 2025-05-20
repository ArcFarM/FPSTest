using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPSSample {
    public class InterAction : MonoBehaviour {
        //상호작용 대상에 부착
        #region Variables
        protected float distance;
        [SerializeField] protected float threshold = 2f;
        //메시지를 띄울 UI
        [SerializeField] protected GameObject actionUI;
        [SerializeField] protected TextMeshProUGUI actionText;
        //애니메이션
        [SerializeField] protected Animator animator;
        //상호작용 가능한 대상 조우시 십자선 활성화
        [SerializeField] protected GameObject crosshair;
        #endregion

        #region Unity Event Methods
        protected void Update() {
            distance = CheckDistance.distance;
            if(distance <= threshold && Input.GetKey(KeyCode.E)) {
                OnInteract();
            }
        }
        protected void OnMouseOver() {
            if (distance <= threshold) {
                ShowActionUI();
            }
            else {
                HideActionUI();
            }
        }
        protected void OnMouseExit() {
            HideActionUI();
        }
        #endregion

        #region Custom Methods
        protected void ShowActionUI() {
            actionUI.SetActive(true);
            crosshair.SetActive(true);
        }
        protected void HideActionUI() {
            actionUI.SetActive(false);
            crosshair.SetActive(false);
        }
        public virtual void OnInteract() {
            //상호작용 시 처리할 내용을 여기에 작성
        }
        #endregion
    }
}