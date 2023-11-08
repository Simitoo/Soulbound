using Cinemachine;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CinemachineZoomController : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;       
        private float originalOS;
        private float targetOS;

        public float zoomAmount = 3.5f;

        private void Start()
        {
            originalOS = virtualCamera.m_Lens.OrthographicSize;
        }

        public void ZoomIn(float animationDuration)
        {
            targetOS = zoomAmount;
            
            StartCoroutine(ZoomCoroutine(animationDuration));

            //StartCoroutine(ResetZoomAfterDelay(targetOS, animationDuration));         
        }

        private IEnumerator ZoomCoroutine(float duration)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float progress = elapsedTime / duration;

                float newOS = Mathf.Lerp(originalOS, targetOS, progress);

                virtualCamera.m_Lens.OrthographicSize = newOS;
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            virtualCamera.m_Lens.OrthographicSize = originalOS;
        }

        private IEnumerator ResetZoomAfterDelay(float target, float delay)
        {
            yield return new WaitForSeconds(delay);

            StartCoroutine(ZoomCoroutine(delay));
        }
    }
}
