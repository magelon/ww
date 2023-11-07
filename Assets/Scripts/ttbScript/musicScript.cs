using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

//init admob
public class musicScript : MonoBehaviour {
	
	void Start () {
		DontDestroyOnLoad (gameObject);
		asgroups = new List<AudioSource> ();
		StartCoroutine("recycle");
    }

    bool canRecycle = false;
	List<AudioSource> asgroups;
	IEnumerator recycle(){
		while (true) {
			yield return new WaitForSeconds(.1f);

			if(asgroups.Count > 30){
				for(int i = 0;i < 15;i++){

					Destroy(asgroups[0]);
					asgroups.RemoveAt(0);
				}
			}
		}
	}

	void OnApplicationPause(bool pauseStatus)
	{ 


	}

	public AudioSource PlayAudioClip(AudioClip clip,bool isloop = false)
	{
		if (clip == null)return null;


		//		AudioSource source = (AudioSource)gameObject.GetComponent("AudioSource");
		//		if (source == null)

		AudioSource	source;

		if (isloop) {
			//bool tExist = false;
			AudioSource[] as1 = GetComponentsInChildren<AudioSource>();
			foreach(AudioSource tas in as1){
				if(tas && tas.clip){
					string clipname = (tas.clip.name);
					if(clipname == clip.name){
						source = tas;
						//tExist = true;
						source.Play();
						return source;
					}
				}
			}
		}

		source = (AudioSource)gameObject.AddComponent<AudioSource>();

		source.clip = clip;source.minDistance = 1.0f;source.maxDistance = 50;source.rolloffMode = AudioRolloffMode.Linear;
		source.transform.position = transform.position;
		source.loop = isloop;
		source.Play();
		if (!isloop) {//not bg
			asgroups.Add (source);
		}
		return source;
	}

}
