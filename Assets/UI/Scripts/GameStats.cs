using UnityEngine;
using System.Collections;

public class GameStats : FadeInOut {
	public PlayerStatRow playerStatRowPrefab;

	void Start () {

	}
	
	protected override void UpdateShow ()
	{
		if(Input.GetKeyDown(KeyCode.Tab))
			Toogle();
	}
}
