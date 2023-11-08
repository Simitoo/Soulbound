using TMPro;
using UnityEngine;

namespace Assets.Scripts.Repository.UI
{
    public class PopUp : MonoBehaviour
    {
        [SerializeField]
        private GameObject messageObject;

        [SerializeField]
        private TextMeshProUGUI popUpMessage;

        public void Show(string message)
        {
            messageObject.SetActive(true);
            popUpMessage.text = message;
            Invoke(nameof(Hide), 2f);
        }

        private void Hide()
        {
            messageObject.SetActive(false);
            popUpMessage.text = null;          
        }


    }
}
