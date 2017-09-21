using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementObserverScript : MonoBehaviour
{

	public AchievementType myType;
	public Text achievementTitleText;
	public Image achievementProgressBar;
	public string achievementName;

	public int achievementMaxCount = 1;
	int achievementCount = 0;

	void Start ()
	{
		InitializeAchievements ();
	}

	void Update ()
	{
		
	}

	public void Notify (AchievementType type)
	{
		if (myType == type) {
			achievementCount++;
			achievementProgressBar.fillAmount = (float)achievementCount / (float)achievementMaxCount;
			if (achievementCount >= achievementMaxCount) {
				AchievementSubjectScript.Instance.UnSubscribeObserver (this);
			}
		}
	}

	public void InitializeAchievements ()
	{
		AchievementSubjectScript.Instance.SubscribeObserver (this);
		achievementTitleText.text = achievementName;
		achievementProgressBar.fillAmount = achievementCount / achievementMaxCount;
	}
}
