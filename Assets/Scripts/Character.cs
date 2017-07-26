using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.SerializableAttribute]
public class Character {

	[SerializeField] private int uid, masterId, level;

	public int UniqueID { get { return uid; } }
	public int MasterID { get { return masterId; } }
	public int Level { get { return level; } }

	public MstCharacter Master {
		get { return MasterDataManager.instance.GetCharacterById (masterId); }
	}

	public int Power { 
		get {
			int power = 0;
			switch (Master.GrowthType) {
			case 1:
				power = (int)(Master.UpperEnergy - ((Master.UpperEnergy - Master.LowerEnergy) * Mathf.Pow((level - Master.MaxLevel)/(1 - Master.MaxLevel), 2)));
				break;
			case 2:
				power = (int)(Master.LowerEnergy + ((level - 1) * (Master.UpperEnergy - Master.LowerEnergy) / (Master.MaxLevel - 1)));
				break;
			case 3:
				power = (int)(Master.LowerEnergy + ((Master.UpperEnergy - Master.LowerEnergy) * Mathf.Pow((level - 1)/(Master.MaxLevel - 1), 2)));
				break;
			}
			return power;
		}
	}

	public Character (int uniqueID, MstCharacter data){
		uid = uniqueID;
		masterId = data.ID;
		level = 1;
	}

	public bool IsLevelMax {
		get { return (level >= Master.MaxLevel) ? true : false; }
	}
}
