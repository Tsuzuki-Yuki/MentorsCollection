using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceManager : SingletonMonoBehaviour<DeviceManager> {

	[SerializeField] private GameObject mainCamera, diveCamera, gvrViewer;

	private AvatarController avatarController;

	public void Setup(){
		Screen.autorotateToPortrait = false;
		Screen.autorotateToLandscapeLeft = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToPortraitUpsideDown = false;
		ToPortrate ();
	}

	public void ToVR (AvatarController avatar = null){
		StartCoroutine (ToVRCoroutine (avatar));
	}

	private IEnumerator ToVRCoroutine(AvatarController avatar){
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		avatarController = avatar;
		mainCamera.SetActive (false);
		mainCamera.GetComponent<Camera> ().enabled = false;
		diveCamera.SetActive (true);
		if (avatar != null) {
			diveCamera.transform.SetParentWithReset (avatar.VRView ());
		}
		yield return null;
		gvrViewer.SetActive (true);
	}

	public void ToPortrate(){
		if (avatarController != null) {
			avatarController.InActiveVR ();
		}
		mainCamera.SetActive (true);
		mainCamera.GetComponent<Camera> ().enabled = true;
		diveCamera.SetActive (false);
		gvrViewer.SetActive (false);
		Screen.orientation = ScreenOrientation.Portrait;
	}

	private void EnableGvrView(){
		gvrViewer.SetActive (true);
	}

	private void UnenableGvrView(){
		gvrViewer.SetActive (false);
	}
}
