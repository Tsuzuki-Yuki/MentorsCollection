using UnityEngine;
using System.Collections;

public class GameManager : SingletonMonoBehaviour<GameManager> {
	
	private void Start() {
		MasterDataManager.instance.LoadData(() => {
			print("ロード終わったお");
			var purchaseView = GameObject.FindObjectOfType<MentorPurchaseView>();
			purchaseView.SetCells();
		});
	}

	public static void Log (object log) {
		if (Debug.isDebugBuild) {
			Debug.Log(log);
		}
	}
}
