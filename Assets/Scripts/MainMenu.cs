using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private GameObject LoadingScreen;
   [SerializeField] private Slider loadingBar;
   public void PlayGame()
   {
   //  SceneManager.LoadScene("Level");
      // AsyncOperation operation =SceneManager.LoadSceneAsync("Level");
      // operation.progress返回0-0.9之间的数    
      StartCoroutine(LoadSceneAsync("Level"));
      
   }
   public void ExitGame()
   {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
   }
   IEnumerator LoadSceneAsync(string sceneName)
   {
      LoadingScreen.SetActive(true);
      AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
      while(!operation.isDone)
      {
         loadingBar.value = operation.progress;
          yield return null;
      }
   }
}
