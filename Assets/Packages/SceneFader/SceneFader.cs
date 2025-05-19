using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace MyDefence
{
    //씬 시작시 페이드인, 씬 종료시 페이드 아웃 효과 구현
    public class SceneFader : MonoBehaviour
    {
        #region Field
        //페이더 이미지
        public Image img;

        //애니메이션 커브
        public AnimationCurve curve;

        //사전 대기 시간
        public float waitTime = 3f;
        #endregion

        private void Start()
        {
            img.color = new Color(0f, 0f, 0f, 1f); //검정색으로 초기화
            //페이드인
           // StartCoroutine(FadeIn(waitTime));
        }

        //코루틴으로 구현
        //FadeIn : 1초동안 : 검정에서 완전 투명으로 (이미지 알파값 a:1 -> a:0)
        IEnumerator FadeIn(float delay = 0f)
        {
            
            if(delay > 0f) {
                yield return new WaitForSeconds(delay); //사전 대기 시간
            }
            float t = 1f;

            while(t > 0)
            {
                t -= Time.deltaTime;
                float a = curve.Evaluate(t);
                img.color = new Color(0f, 0f, 0f, a);

                yield return null;    //한프레임 지연
            }
        }

        //FadeOut : 1초동안 : 투명에서 완전 검정으로 (이미지 알파값 a:0 -> a:1)
        IEnumerator FadeOut()
        {
            yield return new WaitForSeconds(waitTime);
            float t = 0f;

            while(t < 1f)
            {
                t += Time.deltaTime;
                float a = curve.Evaluate(t);
                img.color = new Color(0f, 0f, 0f, a);

                yield return 0f;
            }
        }

        //FadeOut 효과 후 매개변수로 받은 씬이름으로 LoadScene으로 이동
        IEnumerator FadeOut(string sceneName)
        {
            yield return new WaitForSeconds(waitTime);
            //FadeOut 효과 후
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime;
                float a = curve.Evaluate(t);
                img.color = new Color(0f, 0f, 0f, a);

                yield return 0f;
            }

            //씬이동
            if(sceneName != "")
            {
                SceneManager.LoadScene(sceneName);
            }
        }
        //FadeIn 호출
        public IEnumerator StartFadeIn(float delay = 0f) {
            yield return null;
            StartCoroutine(FadeIn(delay));
        }
        //다른 씬으로 이동시 호출
        public void FadeTo(string sceneName = "")
        {            
            StartCoroutine(FadeOut(sceneName));
        }

    }
}