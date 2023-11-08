using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject player;

    private void Awake()
    {
        instance = this; 
    }

    public void KillPlayer()
    {
        //reloadTheScene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //player positio set to last discovered TP position
    }
}
