using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainCameraController : SingletonMonoBehaviour<MainCameraController> {

	[SerializeField] private Transform mainCamera;

	private float
		easing = 6.0f,
		maxSpeed = 2.0f,
		stopDistance = 0.2f;

	private UnityAction onZoomInFinish;
	private AvatarController targetAvatar;
	private Transform target;

	public void ToZoomIn (AvatarController nextTarget, UnityAction OnZoomInFinish){
		targetAvatar = nextTarget;
		target = targetAvatar.MainCameraPoint;
		onZoomInFinish = OnZoomInFinish;
	}

	public void ToZoomOut(){
		onZoomInFinish = null;
		target = this.transform;
	}

	private void Start() {
		target = this.transform;
	}

	private void Update(){
		Vector3 v = Vector3.Lerp (mainCamera.position, target.position, Time.deltaTime * easing) - mainCamera.position;

		mainCamera.rotation = Quaternion.Lerp (mainCamera.rotation, target.rotation, Time.deltaTime * easing);

		if (v.magnitude > maxSpeed) {
			v = v.normalized * maxSpeed;
		}
		mainCamera.position += v;

		float distance = Vector3.Distance (mainCamera.position, target.position);
		if (stopDistance > distance) {
			if (onZoomInFinish != null) {
				onZoomInFinish();
				onZoomInFinish = null;
			}
		}
	}
}
