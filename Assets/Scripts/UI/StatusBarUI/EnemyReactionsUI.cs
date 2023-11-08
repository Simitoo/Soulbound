using Assets.Scripts;
using Assets.Scripts.Enum;
using Assets.Scripts.Reactions;
using UnityEngine;

public class EnemyReactionsUI : MonoBehaviour
{
    private ReactionManager reactionManager;

    public GameObject burnIcon;
    // Start is called before the first frame update
    void Start()
    {
        reactionManager = GetComponent<ReactionManager>();

        reactionManager.onReactionRemoved += RemovedReaction;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateActiveIcons();
    }

    private void UpdateActiveIcons()
    {
        foreach (ElementalReaction reaction in reactionManager.reactions)
        {
            if (reaction.type == ReactionType.Burn)
            {
                burnIcon.SetActive(true);
            }
        }
    }

    private void RemovedReaction(ElementalReaction reaction)
    {
        burnIcon.SetActive(false);
    }
}
