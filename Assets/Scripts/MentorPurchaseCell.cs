using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MentorPurchaseCell : MonoBehaviour {

	[SerializeField] private Image iconImage;
	[SerializeField] private Text
		nameLabel,
		rarityLabel,
		flavorTextLabel,
		productivityLabel,
		costLabel;

	[SerializeField] private Button purchaseButton;
	[SerializeField] private CanvasGroup buttonGroup;

	private bool isSold = false;
	private MstCharacter characterData;

	public void SetValue(MstCharacter data){
		iconImage.sprite = Resources.Load<Sprite> ("Face/" + data.ImageId);
		characterData = data;
		nameLabel.text = data.Name;
		rarityLabel.text = "";
		for (int i = 0; i < data.Rarity; i++) {
			rarityLabel.text += "★";
		}
		flavorTextLabel.text = data.FlavorText;
		productivityLabel.text = "生産性(lv.1) : " + data.LowerEnergy;
		costLabel.text = string.Format ("¥{0:#,0}", data.InitialCost);

		var user = GameManager.instance.User;
		var ch = user.Characters.Find (c => c.MasterID == data.ID);
		isSold = (ch == null) ? false : true;
		if (isSold) SoldView ();
		if (!characterData.PurchaseAvailable (user.Money)) buttonGroup.alpha = 0.5f;
		purchaseButton.onClick.AddListener (() => {
			if (isSold) return;
			if (!characterData.PurchaseAvailable (user.Money)) return;
			isSold = true;
			SoldView ();
			var chara = user.NewCharacter (characterData);
			PortrateUIManager.instance.MentorTrainingView.AddCharacter (chara);
		});
	}

	private void SoldView(){
		buttonGroup.alpha = 0.5f;
		costLabel.text = "sold out";
	}
}
