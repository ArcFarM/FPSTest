using MyDefence;
using System.Collections;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace FPSSample {
    public class Opening : MonoBehaviour {
        #region Variables
        [SerializeField] private GameObject player;
        [SerializeField] private SceneFader sf;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] float delayTime = 3f;
        #endregion

        #region Unity Event Methods
        private void Start() {
            //커서 고정
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            StartCoroutine(StartGame());
        }
        #endregion

        #region Custom Methods
        IEnumerator StartGame() {
            text.enabled = true;
            player.SetActive(false);
            yield return sf.StartFadeIn(delayTime); // FadeIn

            //yield return new WaitForSeconds(delayTime);
            
            while (text.color.a > 0) {
                float new_a = text.color.a - Time.deltaTime;
                text.color = new Color(text.color.r, text.color.g, text.color.b, new_a);
                yield return null;
            }

            text.enabled = false;
            player.SetActive(true);
        }
        #endregion 

    }

}
