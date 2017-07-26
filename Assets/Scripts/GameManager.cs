using UnityEngine;
using System.Collections;

public class GameManager : SingletonMonoBehaviour<GameManager> {

	[SerializeField] private User userData = new User();
	public User User { get { return userData; } }
	private const string SaveKey = "SaveData";

	private void Start() {

		if (PlayerPrefs.HasKey (SaveKey)) {
			userData = JsonUtility.FromJson<User> (PlayerPrefs.GetString (SaveKey));
		}

		MasterDataManager.instance.LoadData(() => {
			PortrateUIManager.instance.SetUp();
		});
	}

	public void Save () {
		PlayerPrefs.SetString (SaveKey, JsonUtility.ToJson (userData));
	}

	public static void Log (object log) {
		if (Debug.isDebugBuild) {
			Debug.Log(log);
		}
	}
}
