using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    public Transform hpBar;
    public Transform nameText;
    public Transform reactionsPanel;

    public Transform uiElementsPosition;

    public Vector3 offset = new Vector3 (0f, 0.2f, 0f);

    private void Update()
    {
        if(hpBar != null && uiElementsPosition != null)
        {
            hpBar.position = uiElementsPosition.position + offset;
            nameText.position = uiElementsPosition.position + offset + offset;
            reactionsPanel.position = uiElementsPosition.position;
        }
    }
}
