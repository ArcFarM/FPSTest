using System.Collections;
using UnityEngine;

namespace FPSSample {
    public enum State {
        Idle, Moving, Attack, Dead
    }

    public class EnemyController : MonoBehaviour {
        #region Variables
        //상태와 애니메이션
        State now_state;
        State last_state;
        [SerializeField] Animator animator;
        [SerializeField] string anim_param = "RobotState";
        //적 능력치
        [SerializeField] static float health = 30;
        float max_health = 30;
        [SerializeField] float move_speed = 5f;
        //적 공격 능력치
        [SerializeField] float attack_delay = 2f;
        float attack_timer = 0f;
        [SerializeField] float attack_range = 2f;
        [SerializeField] float attack_damage = 10f;
        //플레이어를 추적
        [SerializeField] Transform player;
        public float dist = float.MaxValue;
        float threshold = 20f;

        #endregion

        #region Properties
        public static float Health{
            get { return health; }
            set { health = value; }
        }
        #endregion

        #region Unity Event Methods
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start() {
            now_state = State.Idle;
            animator.SetInteger(anim_param, (int)now_state);
        }

        // Update is called once per frame
        void Update() {
            dist = CheckDistance.distance;
            if (attack_timer < attack_delay) attack_timer += Time.deltaTime;

            animator.SetInteger(anim_param, (int)now_state);
            // 상태 머신
            switch (now_state) {
                case State.Idle:
                    if (dist <= threshold)
                        Set_State(State.Moving);
                    break;

                case State.Moving:
                    HandleMoving();
                    if (dist <= attack_range && attack_timer >= attack_delay)
                        Set_State(State.Attack);
                    else if (dist > threshold)
                        Set_State(State.Idle);
                    break;

                case State.Attack:
                    // 코루틴으로 애니메이션과 공격 로직 처리
                    StartCoroutine(DoAttack());
                    // 다음 전환은 코루틴에서 처리하므로 여기서는 대기 상태로
                    now_state = State.Idle;
                    animator.SetInteger(anim_param, (int)now_state);
                    break;

                case State.Dead:
                    // 사망 처리
                    break;
            }
        }
        #endregion

        #region Custom Methods
        public void Set_State(State state) {
            if (state == now_state) return;
            last_state = now_state;
            now_state = state;

            animator.SetInteger(anim_param, (int)now_state);
        }

        void HandleMoving() {
            // 플레이어를 향해 이동
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            transform.position += direction * move_speed * Time.deltaTime;
            // 회전
            transform.LookAt(transform.position);
        }

        public void Set_Health(float amount, bool flag) {
            if (flag) Health += amount;
            else Health -= amount;
            if (Health <= 0) {
                Die();
            }
            else if (Health > max_health) {
                Health = max_health;
            }
        }

        IEnumerator DoAttack() {
            // Attack 상태 진입
            animator.SetTrigger("Attack");
            attack_timer = 0f;

            // 공격 모션 길이만큼 대기
            float animLength = animator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animLength);

            // 실제 데미지 적용
            if (dist <= attack_range) {
                PlayerStats ps = player.GetComponent<PlayerStats>();
                if (ps != null)
                    ps.Set_Health(attack_damage, false);
            }

            // 상태 복귀
            Set_State(State.Idle);
        }

        void Die() {
            Set_State(State.Dead);
            //일정 시간에 걸쳐 투명하게 하고, 완전히 투명하면 삭제
            Destroy(gameObject, 4f);
            gameObject.SetActive(false);
        }
        
        #endregion
    }
}


