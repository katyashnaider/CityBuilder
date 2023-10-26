using DG.Tweening;
using Scripts.Sounds;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void OpenMenu(int numberScene)
    {
        SceneManager.LoadSceneAsync(numberScene);
        SoundManager.Instance.StopSoundGame();
        DOTween.KillAll(); 
        //LoadProgress("LevelNumber");
       // StopAllCoroutines();
        //SoundManager.Instance.PlaySoundMainMenu(_clipGame);
    }
}