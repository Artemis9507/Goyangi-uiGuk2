using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FIndSpawnPoint : MonoBehaviour
{
   [SerializeField] private Transform player;
   private GameObject spawnPoint;

   private void OnEnable()
   {
      SceneManager.sceneLoaded += SceneLoaded;
   }

   private void OnDisable()
   {
      SceneManager.sceneLoaded -= SceneLoaded;
   }

   private void SceneLoaded(Scene scene, LoadSceneMode mode)
   {
      if (scene.buildIndex == 2)
      {
         spawnPoint = GameObject.Find("SpawnPoint");
         player.SetPositionAndRotation(spawnPoint.transform.position, player.transform.rotation);
         Physics.SyncTransforms();
      }
   }
   
}
