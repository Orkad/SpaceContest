using UnityEngine;
using System.Collections;

public abstract class Building : MonoBehaviour{
	[Multiline]
	public string _description;
	public Sprite _icon;
	public int level = 0;
	public int maxLevel = 10;
	public float costPerLevel;
	
	public string description{get{return _description;}}
	public Sprite icon{get{return _icon;}}
	public float cost{get{return costPerLevel * level;}}

	public bool Available(){
		if(level < maxLevel)
			return true;
		return false;
	}
	
	public void BuyEffect(){
		LevelUp ();
	}
	
	/// <summary>
	/// Incrémente le niveau du batiment de 1
	/// </summary>
	public void LevelUp(){
		if(Available()){
			level++;
		}
	}
	
	/// <summary>
	/// Décrémente le niveau du batiment de 1
	/// </summary>
	public void LevelDown(){
		if(level > 0)
			level--;
	}
}
