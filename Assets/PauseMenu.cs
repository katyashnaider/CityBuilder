using Scripts.Sounds;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _clipGame;
    
    public void OpenMenu(int numberScene)
    {
        SceneManager.LoadSceneAsync(numberScene);
        SoundManager.Instance.StopSoundGame();
        //SoundManager.Instance.PlaySoundMainMenu(_clipGame);
    }

    public void RestartLevel() => 
        Debug.Log("рестарт уровня");
}
