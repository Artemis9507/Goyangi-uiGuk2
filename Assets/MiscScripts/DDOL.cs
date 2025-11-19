using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour
{
   public static DDOL Instance;

   private void Awake()
   {
      if (Instance != null)
      {
         Destroy(gameObject);
         return;
      }

      Instance = this;
      DontDestroyOnLoad(gameObject);

      SceneManager.sceneLoaded += OnSceneLoaded;
   }

   private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
   {
      if (scene.buildIndex == 0) 
      {
         Destroy(gameObject);
         Instance = null;
      }
   }
}
