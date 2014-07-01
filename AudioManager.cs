using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
//	public static AudioManager Init(){
//		GameObject gobj = new GameObject();
//		gobj.name = "AudioManager";
//		return gobj.AddComponent<AudioManager>();
//		DontDestroyOnLoad(gobj);
//	}

//	public static AudioManager instance;

	public static void PlaySound(string path, bool loop = false, string name = "AudioSource"){
		AudioClip ac = Resources.Load(path) as AudioClip;
		PlaySound(ac, loop, name);
	}
	public static GameObject PlaySound(AudioClip ac, bool loop = false, string name = "AudioSource"){
		if(ac == null){
			Debug.LogWarning("Play Sound AC Is Null");
			return null;
		}
		GameObject gobjSound = new GameObject();
		gobjSound.name = name;
		AudioSource audioS = gobjSound.AddComponent<AudioSource>();
		audioS.clip = ac;
		audioS.loop = loop;

		if((loop && GameManager.enableMusic) || (!loop && GameManager.enableSound)){
			audioS.Play();
		}

		if(!loop){
			Destroy (gobjSound, ac.length);
		}

		return gobjSound;
	}

	public static void StopSound(string name){
		GameObject gobjSound = GameObject.Find(name);
		if(gobjSound != null){
			AudioSource audio = gobjSound.GetComponent<AudioSource>();
			audio.Stop();
		}
	}

	public static bool ContineSound(string name){
		bool success = false;
		GameObject gobjSound = GameObject.Find(name);
		if(gobjSound != null){
			AudioSource audio = gobjSound.GetComponent<AudioSource>();
			if(!audio.isPlaying){
				audio.Play();
			}
			success = true;
		}

		return success;
	}
}
