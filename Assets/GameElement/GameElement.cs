using UnityEngine;
using UnityEngine.Networking;
using System;

public abstract class GameElement : NetworkBehaviour {

    #region Properties

    /// <summary>
    /// Identité sur le réseau
    /// </summary>
    public NetworkIdentity networkID { get { return GetComponent<NetworkIdentity>(); } }

    /// <summary>
    /// Autorise la selection multiple
    /// </summary>
    public bool multipleSelection = false;

    /// <summary>
    /// Objet graphique indiquant la selection de l'element
    /// </summary>
    public SelectionIndicator selectionIndicator;
	
    /// <summary>
    /// Rayon de l'element
    /// </summary>
	public float radius{get{return transform.localScale.x/2;}set{transform.localScale = new Vector3(value/2,value/2,value/2);}}

    #endregion

    #region Methodes

    /// <summary>
    /// Si le GameElement appartient au joueur
    /// </summary>
    /// <returns></returns>
    public bool IsMine()
    {
        return networkID.hasAuthority;
    }

    /// <summary>
    /// Si le GameElement n'appartient a personne
    /// </summary>
    /// <returns></returns>
    public bool IsNeutral()
    {
        return networkID.clientAuthorityOwner == null;
    }

    /// <summary>
    /// Changement de propriétaire
    /// </summary>
    /// <param name="conn"></param>
    [Server]
    public void ChangeOwner(NetworkConnection conn)
    {
        networkID.AssignClientAuthority(conn);
    }

    [Server]
    public void ChangeOwner(Player player)
    {
        networkID.AssignClientAuthority(player.connectionToClient);
    }

    #endregion

    #region Interaction

    /// <summary>
    /// Evenement lorsque le joueur selectionne le GameElement
    /// </summary>
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
	
    /// <summary>
    /// Evenement lorsque le joueur deselectionne le GameElement
    /// </summary>
	public virtual void OnDeselect(){
		if(selectionIndicator)
			selectionIndicator.gameObject.SetActive(false);
	}

	/// <summary>
    /// Evenement lorsque la souris survolle le GameElement
    /// </summary>
	public virtual void OnMouseEnter(){}
	
    /// <summary>
    /// Evenement lorsque la souris sort du GameElement
    /// </summary>
	public virtual void OnMouseExit(){}

    /// <summary>
    /// Action sur un endroit vide lorsque selectionné
    /// </summary>
    /// <param name="position">Position de l'action</param>
    public virtual void Action(Vector3 position){
		
	}
	
    /// <summary>
    /// Action sur un element lorsque selectionné
    /// </summary>
    /// <param name="otherElement">Element concerné</param>
	public virtual void Action(GameElement otherElement){
		
	}

    #endregion

}
