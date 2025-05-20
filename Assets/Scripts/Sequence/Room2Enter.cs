using UnityEngine;
using System.Collections;
using TMPro;

namespace FPSSample {
    public class Room2Enter : MonoBehaviour {
        #region Variables
        float delayTime = 1f;
        [SerializeField] TextMeshProUGUI textDisplay; //출력할 문구
        [SerializeField] GameObject arrowObj; //보여줄 강조 화살표
        bool onceFlag = false; //한번만 실행하기 위한 플래그
        #endregion

        #region Unity Event Methods
        private void OnTriggerEnter(Collider other) {
            if(onceFlag) StartCoroutine(SequncePlay(other.gameObject));
            onceFlag = false;   
        }
        #endregion

        #region Custom Methods
        IEnumerator SequncePlay(GameObject go) {
            //1. 플레이어 잠시 비활성화
            go.SetActive(false);

            //2. 텍스트 출력
            textDisplay.gameObject.SetActive(true);
            textDisplay.CrossFadeAlpha(1, 1f, false); // 텍스트 서서히 나타나기
            //3. 화살표 활성화
            yield return new WaitForSeconds(delayTime); // 대기
            arrowObj.SetActive(true);
            //4. 캐릭터 활성화
            yield return new WaitForSeconds(delayTime); // 대기
            go.SetActive(true);
            //5. 잠시 후 텍스트 다시 끄기
            yield return new WaitForSeconds(delayTime * 2); // 대기
            textDisplay.CrossFadeAlpha(0, 1f, false); // 텍스트 서서히 사라지기
        }
        #endregion
    }
}
