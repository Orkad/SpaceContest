using UnityEngine;
using System.Collections;

public interface IItem{
	string name{get;}
	string description{get;}
	Sprite icon{get;}
}

public interface ICostable:IItem{
	float cost{get;}
	bool Available();
	void BuyEffect();
}

public static class ICostableExtention{
	public static bool CanBuy(this Planet p,ICostable c){
		if(c.Available() && p.resources > c.cost)
			return true;
		return false;
	}
}