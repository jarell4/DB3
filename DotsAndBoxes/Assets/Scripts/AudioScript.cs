using UnityEngine;
using System.Collections;

public class AudioScript : MonoBehaviour {

	//FileVars
	public AudioClip dotAudio;
	public AudioClip lineAudio;
	public AudioClip boxAudio;

	//StaticVars
	public static AudioClip Dot_SFX;
	public static AudioClip Line_SFX;
	public static AudioClip Box_SFX;

	//AudioSource
	public static AudioSource SFXSource;

	// Use this for initialization
	void Start () {
	
		SFXSource = GameObject.Find("audioHolder").GetComponent<AudioSource>();

		Dot_SFX = dotAudio;
		Line_SFX = lineAudio;
		Box_SFX = boxAudio;
	}

	public static void PlayAudio(string objectTag)
	{
		AudioClip sfxClip = Dot_SFX;
		float sfxVolume = 0.8f;

		switch (objectTag.ToLower())
		{
		case "dot":
			sfxClip = Dot_SFX;
			break;

		case "line":
			sfxClip = Line_SFX;
			sfxVolume = 1f;
			break;

		case "box":
			sfxClip = Box_SFX;
			break;
		}

		if(sfxClip != null)
			SFXSource.PlayOneShot(sfxClip, sfxVolume);
	}
}
