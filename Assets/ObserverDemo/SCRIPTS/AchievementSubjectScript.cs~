using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AchievementType
{
	ACHIEVEMENT_1 = 0,
	ACHIEVEMENT_2,
	ACHIEVEMENT_3,
	ACHIEVEMENT_4,
	ACHIEVEMENT_5,
	ACHIEVEMENT_6,
}

public class AchievementSubjectScript : MonoBehaviour
{
	public static AchievementSubjectScript Instance;

	List <AchievementObserverScript> observerList = new List<AchievementObserverScript> ();

	public void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else if (Instance != this) {
			Destroy (this.gameObject);
		}

		DontDestroyOnLoad (this.gameObject);
	}

	void Start ()
	{

	}

	void Update ()
	{
		ButtonInteract ();
	}

	public void ButtonInteract ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			Notify (AchievementType.ACHIEVEMENT_1);
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			Notify (AchievementType.ACHIEVEMENT_2);
		}
		if (Input.GetKeyDown (KeyCode.Alpha9)) {
			Notify (AchievementType.ACHIEVEMENT_3);
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			Notify (AchievementType.ACHIEVEMENT_4);
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			Notify (AchievementType.ACHIEVEMENT_5);
		}
	}

	public void Notify (AchievementType type)
	{
		for (int i = 0; i < observerList.Count; i++) {
			observerList [i].Notify (type);
		}
	}

	public void SubscribeObserver (AchievementObserverScript observerScript)
	{
		observerList.Add (observerScript);
	}

	public void UnSubscribeObserver (AchievementObserverScript observerScript)
	{
		observerList.Remove (observerScript);
		Notify (AchievementType.ACHIEVEMENT_6);
	}


}
