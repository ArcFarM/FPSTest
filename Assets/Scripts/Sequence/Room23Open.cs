using System.Collections;
using UnityEngine;

namespace FPSSample {
    public class Room23Open : MonoBehaviour {
        //문 열기 상호작용 대상에 부착
        #region Variables
        AudioSource door_as;
        [SerializeField] BoxCollider door_trg;
        [SerializeField] AudioClip enemy_sound;
        Animator door_anim;
        string anim_param = "isOpen";
        #endregion
        #region Unity Event Methods
        private void Start() {
            door_as = GetComponent<AudioSource>();
            door_anim = GetComponent<Animator>();
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
            //적 등장 소리 재생
            door_as.clip = enemy_sound;
            door_as.Play();
            yield return new WaitForSeconds(1f);
            go.SetActive(true);
            //TODO : 적 등장 구현, 다른 스크립트에서 처리
            door_trg.enabled = false;
        }
        #endregion
    }
}
