using UnityEngine;
using System.Collections;

public enum OwnerMask{
	self,
	enemy,
	all,
	none
}
public enum SpellType{
	LineSkillShot
}

public class Spell : MonoBehaviour {
	private Planet caster;
	private Vector3? fixedTarget;
	private ISelectable dynamicTarget;
	
	public OwnerMask ownerMask;
	public SpellType spellType;
	public float range;
	public float thickness;
	public float duration;
	public float damages;
	
	void Update(){
		switch(spellType){
			case SpellType.LineSkillShot:
				break;
		}
	}
	
	public void Cast(ISelectable target){
		
	}
}

//Offessif
//Spell missile nucléaire (projectile) dégat de zone
//Spell Laser (skillshot)

//Défenssif
//Spell Turtle (Bouclier pendant x seconde)
