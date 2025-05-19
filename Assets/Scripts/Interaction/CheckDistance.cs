using UnityEngine;

namespace FPSSample {
    public class CheckDistance : MonoBehaviour {
        #region Variables
        //플레이어가 찾을 대상까지의 거리
        public static float distance = 5f;
        //레이캐스트
        RaycastHit hit;
        //상호작용 가능한 대상만 찾기
        [SerializeField] LayerMask targetLayer;

        #endregion
        #region Unity Event Methods
        private void Start() {
            distance = Mathf.Infinity;
        }
        private void Update() {
            if(Physics.Raycast(transform.position, transform.forward, out hit)) {
                if (hit.collider != null) {
                    distance = hit.distance;
                }
            }
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            if (Physics.Raycast(transform.position, transform.forward, out hit)) {
                if (hit.collider != null) {
                    distance = hit.distance;
                    Gizmos.DrawRay(transform.position, transform.forward * distance);
                }
            }
        }
        #endregion
    }
}

