﻿using System.Collections;
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
		get { return 1; }
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
