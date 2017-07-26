using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UniRx;

public class PortrateUIManager : SingletonMonoBehaviour<PortrateUIManager> {

	[SerializeField] private MentorPurchaseView mentorPurchaseView;
	[SerializeField] private MentorTrainingView mentorTrainingView;
	public MentorPurchaseView MentorPurchaseView { get { return mentorPurchaseView; } }
	public MentorTrainingView MentorTrainingView { get { return mentorTrainingView; } }

	[SerializeField] private Button workButton, dataClearButton;
	[SerializeField] private Text
		moneyLabel,
		autoWorkLabel,
		employeeCountLabel,
		productivityLabel;

	[SerializeField] private Const.View
		currentView = Const.View.Close,
		lastView = Const.View.Purchase;

	private static User User { get { return GameManager.instance.User; } }

	public void SetUp() {
		mentorPurchaseView.SetCells ();
		mentorTrainingView.SetCells ();

		openButton.onClick.AddListener (() => {
			openButton.gameObject.SetActive (false);
			ChangeView (lastView);
		});

		UpdateView ();

		workButton.onClick.AddListener (() => {
			var power = User.Characters.Sum (c => c.Power);
			if (power == 0) power = 1;
			User.AddMoney (power);
			UpdateView ();
		});

		dataClearButton.onClick.AddListener (() => {
			PlayerPrefs.DeleteAll ();
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Main");
		});

		User.Money.Subscribe (_ => {
			UpdateView ();
		});
	}

	public void UpdateView(){
		moneyLabel.text = string.Format ("¥{0:#,0}", User.Money);
		employeeCountLabel.text = string.Format ("{0:#,0}人", User.Characters.Count);
		productivityLabel.text = string.Format ("¥{0:#,0}", User.ProductivityPerTap);
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
