using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PortrateUIManager : SingletonMonoBehaviour<PortrateUIManager> {

	[SerializeField] private MentorPurchaseView mentorPurchaseView;
	[SerializeField] private MentorTrainingView mentorTrainingView;
	public MentorPurchaseView MentorPurchaseView { get { return mentorPurchaseView; } }
	public MentorTrainingView MentorTrainingView { get { return mentorTrainingView; } }

	[SerializeField] private Text
		moneyLabel,
		autoWorkLabel,
		employeeCountLabel,
		productivityLabel;

	[SerializeField] private Const.View
		currentView = Const.View.Close,
		lastView = Const.View.Purchase;

	public void SetUp() {
		mentorPurchaseView.SetCells ();
		mentorTrainingView.SetCells ();

		openButton.onClick.AddListener (() => {
			openButton.gameObject.SetActive (false);
			ChangeView (lastView);
		});
	}

	public void ChangeView (Const.View nextView){
		if (currentView == nextView) {
			return;
		}

		lastView = currentView;
		currentView = nextView;
		isMoving = true;
		switch (nextView) {
		case Const.View.Purchase:
			mentorPurchaseView.gameObject.SetActive (true);
			mentorTrainingView.gameObject.SetActive (false);
			break;
		case Const.View.Training:
			mentorPurchaseView.gameObject.SetActive (false);
			mentorTrainingView.gameObject.SetActive (true);
			break;
		case Const.View.Close:
			openButton.gameObject.SetActive (true);
			break;
		}
	}

	[SerializeField] private Button openButton;
	[SerializeField] private Transform
		mainPanel,
		openPoint,
		closePoint;
	private bool isMoving = false;
	private float
		easing = 8.0f,
		maxSpeed = 3.0f,
		stopDistance = 0.001f;

	private void Update() {
		if (!isMoving) {
			return;
		}
		var target = (currentView == Const.View.Close) ? closePoint : openPoint;

		Vector3 v = Vector3.Lerp (mainPanel.position, target.position, Time.deltaTime * easing) - mainPanel.position;
		if (v.magnitude > maxSpeed) {
			v = v.normalized* maxSpeed;
		}
		mainPanel.position += v;

		if (isMoving) {
			float distance = Vector3.Distance (mainPanel.position, target.position);
			if (stopDistance > distance) {
				isMoving = false;
			}
		}
	}
}
