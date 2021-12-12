
using UnityEngine.Audio;
using UnityEngine;

public static class AudioManager: MonoBehaviour {
   public static void PlaySound() {
    GameObject soundGameObject = new GameObject("Sound");
    AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
  }
}
