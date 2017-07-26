using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class User {

	[SerializeField] private int money;
	[SerializeField] private List<Character> characters;

	public int Money { get { return money; } }
	public List<Character> Characters { get { return characters ?? (characters = new List<Character> ()); } }

	public Character NewCharacter(MstCharacter data){
		var uniqueId = (Characters.Count == 0) ? 1 : characters [characters.Count - 1].UniqueID + 1;
		var chara = new Character (uniqueId, data);
		characters.Add (chara);
		money -= data.InitialCost;
		return chara;
	}
}
