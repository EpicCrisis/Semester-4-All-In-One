using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IBaseNPC
{
	void Move (Vector2 moveDestination);

	void Selected (bool t);
}

public class NPCGroup : IBaseNPC
{
	public List<IBaseNPC> memberList = new List<IBaseNPC> ();

	public void Move (Vector2 moveDestination)
	{
		for (int i = 0; i < memberList.Count; i++) {
			memberList [i].Move (moveDestination);
		}
	}

	public void Selected (bool t)
	{
		for (int i = 0; i < memberList.Count; i++) {
			memberList [i].Selected (t);
		}
	}

	public void AddMember (IBaseNPC newMember)
	{
		memberList.Add (newMember);
	}

	public void RemoveMember (IBaseNPC removeMember)
	{
		memberList.Remove (removeMember);
	}

	public void ClearMembers ()
	{
		memberList.Clear ();
	}
}

[System.Serializable]
public class BoundKey
{
	public bool isBound = false;
	public Image boundKeyStatusImage;
	public Text unitAmountText;
	public NPCGroup boundGroup = new NPCGroup ();

	public void Initialise ()
	{
		if (!isBound) {
			boundKeyStatusImage.color = Color.red;
		} else {
			boundKeyStatusImage.color = Color.green;
		}
		unitAmountText = boundKeyStatusImage.GetComponentInChildren<Text> ();
	}

	public void BindGroup (NPCGroup bindingGroup)
	{
		isBound = true;
		boundGroup.ClearMembers ();
		for (int i = 0; i < bindingGroup.memberList.Count; i++) {
			boundGroup.AddMember (bindingGroup.memberList [i]);
		}
		//boundGroup = bindingGroup;
		UpdateKey ();
	}

	public void UpdateKey ()
	{
		if (!isBound) {
			boundKeyStatusImage.color = Color.red;
		} else {
			boundKeyStatusImage.color = Color.green;
		}

		if (boundGroup.memberList.Count > 0) {
			unitAmountText.text = boundGroup.memberList.Count.ToString ();
		}
	}
}

public class NPCSelectionManagerScript : MonoBehaviour
{

	public static NPCSelectionManagerScript Instance;
	public bool isBinding = true;
	NPCGroup selectedUnits = new NPCGroup ();
	public BoundKey[] boundList = new BoundKey[5];

	void Awake ()
	{
		Instance = this;
	}

	void Start ()
	{
		for (int i = 0; i < boundList.Length; i++) {
			boundList [i].Initialise ();
		}
	}

	void Update ()
	{
		if (Input.GetMouseButton (1)) {
			Vector2 tempMoveTarget = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			selectedUnits.Move (tempMoveTarget);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("BINDING TOGGLE");
			isBinding = !isBinding;
		}

		if (isBinding) {
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				boundList [0].BindGroup (selectedUnits);
			}
			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				boundList [1].BindGroup (selectedUnits);
			}
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				boundList [2].BindGroup (selectedUnits);
			}
			if (Input.GetKeyDown (KeyCode.Alpha4)) {
				boundList [3].BindGroup (selectedUnits);
			}
			if (Input.GetKeyDown (KeyCode.Alpha5)) {
				boundList [4].BindGroup (selectedUnits);
			}
		} else {
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				UpdateSelectedGroup (boundList [0].boundGroup);
			}
			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				UpdateSelectedGroup (boundList [1].boundGroup);
			}
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				UpdateSelectedGroup (boundList [2].boundGroup);
			}
			if (Input.GetKeyDown (KeyCode.Alpha4)) {
				UpdateSelectedGroup (boundList [3].boundGroup);
			}
			if (Input.GetKeyDown (KeyCode.Alpha5)) {
				UpdateSelectedGroup (boundList [4].boundGroup);
				//selectedUnits = boundList [4].boundGroup;
			}
		}
	}

	void UpdateSelectedGroup (NPCGroup tempGroup)
	{
		selectedUnits.Selected (false);
		selectedUnits.ClearMembers ();
		for (int i = 0; i < tempGroup.memberList.Count; i++) {
			selectedUnits.AddMember (tempGroup.memberList [i]);
		}
		selectedUnits.Selected (true);
	}

	public void CreateGroup (List<IBaseNPC> members)
	{
		selectedUnits.ClearMembers ();
		for (int i = 0; i < members.Count; i++) {
			selectedUnits.AddMember (members [i]);
		}
	}
}
