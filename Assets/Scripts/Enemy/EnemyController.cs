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
        [SerializeField] float move_speed = 5f;
        [SerializeField] float attack_delay = 2f;
        float attack_timer = 0f;
        [SerializeField] float attack_range = 2f;
        //플레이어를 추적
        [SerializeField] Transform player;
        float dist = float.MaxValue;
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
        }

        // Update is called once per frame
        void Update() {
            if(attack_timer < attack_delay) attack_timer += Time.deltaTime;

            if (dist > threshold) {
                //플레이어와의 거리가 멀어지면 Idle 상태로 전환
                Set_State(State.Idle);
            } else Set_State(State.Moving);

            if (now_state == State.Moving) {
                //플레이어와의 거리 계산
                dist = Vector3.Distance(transform.position, player.position);

                //플레이어를 향해 이동
                Vector3 dir = player.position - transform.position;
                transform.Translate(dir.normalized * move_speed * Time.deltaTime, Space.World);
                //플레이어를 바라보게 회전
                transform.LookAt(player.position);

                if (dist <= attack_range && attack_timer >= attack_delay) {
                    Set_State(State.Attack);
                    attack_timer = 0f;
                }
            }
            else if (now_state == State.Attack) {
                //공격 애니메이션 재생
                animator.SetTrigger("Attack");
                Set_State(State.Moving);
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

        public void Set_Health(float amount) {
            Health -= amount;
            if (Health <= 0) {
                Die();
            }
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


