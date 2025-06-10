using System.Collections;
using UnityEngine;

namespace FPSSample {
    public class PlayerStats : MonoBehaviour {
        #region Variables
        //플레이어 능력치
        [SerializeField] static float health = 30;
        float max_health = 30;

        //연출용 animator
        Animator animator;
        #endregion
        #region Properties        
        public static float Health {
            get { return health; }
            set { health = value; }
        }
        public static bool isDead {
            get { return health <= 0; }
        }
        #endregion
        #region Unity Event Methods 
        private void Start() {
            health = max_health;
            animator = GetComponent<Animator>();
        }
        #endregion
        #region Custom Methods
        IEnumerator Get_Hurt() {
            //피격 애니메이션 재생
            yield return new WaitForSeconds(1f);
            animator.SetTrigger("GetHit");
        }
        public void Set_Health(float value, bool isDamage) {
            if (isDamage) {
                StartCoroutine(Get_Hurt());
                health -= value;
                if (health <= 0) {
                    health = 0;
                    //플레이어 사망 처리
                    Debug.Log("Player Dead");
                }
            }
            else {
                health += value;
                if (health > max_health) health = max_health;
            }
        }
        #endregion
    }

}
