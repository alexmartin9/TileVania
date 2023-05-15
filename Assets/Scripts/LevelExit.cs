using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelExit : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 2f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            StartCoroutine(LoadNextLevel());
        }       
     }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(loadLevelDelay);
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIdx = currentSceneIdx + 1;
        if (nextSceneIdx == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIdx = 0;
        }

        ScenePersist scenePersist = FindObjectOfType<ScenePersist>();
        scenePersist.ResetScenePersist();

        SceneManager.LoadScene(nextSceneIdx);
    }


}
