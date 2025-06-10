using System.Collections;
using UnityEngine;

namespace FPSSample {
    public class Room23Open : MonoBehaviour {
        //문 열기 상호작용 대상에 부착
        #region Variables
        [SerializeField] AudioSource door_as;
        [SerializeField] BoxCollider door_trg;
        [SerializeField] Animator door_anim;

        string anim_param = "IsOpen";
        //생성할 적
        [SerializeField] AudioSource enemy_sound;
        [SerializeField] EnemyController enemy;
        
        #endregion
        #region Unity Event Methods
        private void Start() {
            door_as = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player")) {
                StartCoroutine(DoorAction(other.gameObject));
            }
        }
        #endregion
        #region Custom Methods
        IEnumerator DoorAction(GameObject go) {
            //잠시 캐릭터 비활성화
            go.SetActive(false);
            //문 열림 애니메이션 재생하고 소리 출력
            door_as.Play();
            door_anim.SetBool(anim_param, true);
            yield return new WaitForSeconds(1f);
            door_trg.enabled = false;
            StartCoroutine(EnemyCreation(go));
        }
        IEnumerator EnemyCreation(GameObject go) {
            //적 등장 소리 재생
            enemy_sound.Play();
            //적 생성
            enemy.gameObject.SetActive(true);
            if(enemy != null) enemy.Set_State(State.Moving);
            yield return new WaitForSeconds(1f);
            //플레이어 다시 활성화
            go.SetActive(true);

        }
        #endregion
    }
}
