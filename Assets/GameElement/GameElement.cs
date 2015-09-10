using UnityEngine;
using UnityEngine.Networking;


public abstract class GameElement : NetworkBehaviour,IOwnable,ISelectable {
	
	//OWNER
	[SyncVar(hook="OnOwnerChange")]
	public uint _owner;
	public uint owner{
		get{
			return _owner;
		}
		set{
			_owner = value;
		}
	}
    public GameObject GetPlayerOwner()
    {
        return Player.FindPlayerByID(owner).gameObject;
    }
    public virtual void OnOwnerChange(uint owner){ Debug.Log("Owner Changed"); }
	
	//RADIUS
	public float radius{
		get{
			return transform.localScale.x/2;
		}
		set{
			transform.localScale = new Vector3(value/2,value/2,value/2);
		}
	}
	
	public bool multipleSelection = false;
	
	public SelectionIndicator selectionIndicator;
	
	public virtual void OnSelect(){
		if(selectionIndicator){
			if(this.IsNeutral())
				selectionIndicator.ChangeColor(Player.neutralColor);
			else if(this.IsMine())
				selectionIndicator.ChangeColor(Player.myColor);
			else
				selectionIndicator.ChangeColor(Player.enemyColor);
			selectionIndicator.gameObject.SetActive(true);
		}
	}
	
	public virtual void OnDeselect(){
		if(selectionIndicator)
			selectionIndicator.gameObject.SetActive(false);
	}
	
	public virtual void OnMouseEnter(){}
	
	public virtual void OnMouseExit(){}
	
	//Correspond au clic droit
	public virtual void Action(Vector3 position){
		
	}
	
	public virtual void Action(GameElement otherElement){
		
	}
}
