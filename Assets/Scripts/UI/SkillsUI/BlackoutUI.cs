using Assets.Scripts.Skills;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class BlackoutUI : MonoBehaviour
    {
        private PlayerManager playerManager;
        private SkillController skillController;

        private float duration;
        public GameObject blackoutPanel;

        private void Start()
        {
            playerManager = PlayerManager.instance;
            skillController = playerManager.player.GetComponent<SkillController>();

            duration = 0.5f;

            skillController.onSkillCast += Blackout;
        }

        private void Blackout(Skill skill)
        {
            duration += skill.AnimationDuration;

            StartCoroutine(StartBlackout());
        }

        private IEnumerator StartBlackout()
        {
            blackoutPanel.SetActive(true);

            yield return new WaitForSeconds(duration);

            blackoutPanel.SetActive(false);
        }
    }
}
