using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Attach this script to an empty gameobject.
/// </summary>
[RequireComponent (typeof (Image))]
public class ScreenFader : MonoBehaviour {

	float startTimer, endTimer;

	[Tooltip("Speed of fading in/out.")]
	public float fadeConstant;

	[Tooltip("UI image of the fading curtain. There is no necessity to have a source image. One may adjust the color of image component.")]
	public Image fadeScreen;

	public static string sceneToLoad;
	bool fadedIn;

	public static screenFader instance;

	// Use this for initialization
	void Start () {
		instance = this;
		InvokeRepeating ("fadeIn", 0, 0.01f);
		StartCoroutine (cancelFadeIn ());
	}

	public void fadeIn(){
		startTimer += 0.01f;
		fadeScreen.color = Color.Lerp (Color.black, Color.clear, fadeConstant * startTimer);
		if (fadeScreen.color == Color.clear)
			fadedIn = true;
	}

	public void fadeOut(){
		endTimer += 0.01f;
		fadeScreen.color = Color.Lerp (Color.clear, Color.black, fadeConstant * endTimer);
		if (fadeScreen.color == Color.black)
			SceneManager.LoadScene (sceneToLoad);
	}

	IEnumerator cancelFadeIn(){
		yield return new WaitUntil (() => fadedIn);
		CancelInvoke ("fadeIn");
	}

	/// <summary>
	/// Fades the screen out and loads new scene.
	/// </summary>
	/// <param name="sceneName">Scene to load.</param>
	public static void FadeOutAndLoadNewScene(string sceneName){
		sceneToLoad = sceneName;
		instance.InvokeRepeating ("fadeOut", 0, 0.01f);
	}
}
