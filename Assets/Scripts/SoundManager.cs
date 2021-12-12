
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager: MonoBehaviour {
   public static void PlaySound() {
    GameObject soundGameObject = new GameObject("Sound");
    AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
  }
}
