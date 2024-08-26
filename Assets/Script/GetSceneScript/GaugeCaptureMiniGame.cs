using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

namespace MiniGames
{
    public class GaugeCaptureMiniGame : MonoBehaviour, ICaptureMiniGame
    {
        public UnityEngine.UI.Image gaugeImage;
        public float fillSpeed = 1.0f;

        private bool isRunning = false;
        private System.Action<bool> onCompleteCallback;

        public void StartMiniGame(System.Action<bool> onComplete)
        {
            onCompleteCallback = onComplete;
            isRunning = true;
            gaugeImage.fillAmount = 0f;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if (!isRunning) return;

            gaugeImage.fillAmount += fillSpeed * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isRunning = false;
                gameObject.SetActive(false);

                bool isSuccess = gaugeImage.fillAmount >= 0.9f;
                onCompleteCallback?.Invoke(isSuccess);
            }

            if (gaugeImage.fillAmount >= 1f)
            {
                gaugeImage.fillAmount = 0f;
            }
        }
    }
}
