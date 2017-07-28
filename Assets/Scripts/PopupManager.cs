using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopupManager : SingletonMonoBehaviour<PopupManager> {

	public bool isOpened { get { return popupList.Count > 0 ? true : false; } }

	[SerializeField] private GameObject commonPopup;
	[SerializeField] private GameObject descriptionPopup;

	private List<IPopupController> popupList = new List<IPopupController>();

	public void RemoveLastPopup(){
		if (popupList.Count > 0) {
			popupList.RemoveAt (popupList.Count - 1);
		}
	}

	public void OpenCommon(string SendMessage, UnityAction onCloseFinish = null){
		var popup = CreatePopup (commonPopup);
		var popupController = popup.GetComponent<CommonPopupController> ();
		popupController.SetValue (SendMessage, onCloseFinish);
		popupList.Add (popupController);
		popupController.Open (null);
	}

	public void OpenDescription(Character data, UnityAction onCloseFinish = null){
		var popup = CreatePopup (descriptionPopup);
		var popupController = popup.GetComponent<DescriptionPopupController> ();
		popupController.SetValue (data, onCloseFinish);
		popupList.Add (popupController);
		popupController.Open (null);
	}

	public GameObject CreatePopup (GameObject prefab){
		GameObject popup = Instantiate (prefab);
		popup.transform.SetParentWithReset (this.transform);
		return popup;
	}
}
