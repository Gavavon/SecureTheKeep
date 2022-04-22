using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
	[Header("Dialogue Mechanics")]
	public string[] tutorialTexts =
	{
		"Welcome back Wizard! Our fort is under siege but your here so we should be good.",
		"Thanks to the magic artifacts you left us we can now speak to you anywhere and track the number of enemies around the fort.",
		"Enemies like this one on the way have been attacking the fort.",
		"This one seems to be pretty weak. However, enemies have been showing up by the dozens.",
		"You can attack them with your magic but be careful the our magic source you pull from is a bit drained.",
		"For now some of your more powerful spells have a cool down.",
		"Additionally we have are construction works making repairs to the gate after every wave of enemies.",
		"We get a longer break every 10 waves so we can bring back broken walls and repair them more then.",
		"It seems more enemies are coming get ready Wiz."
	};
	public static TutorialManager instance;
	private void Awake()
	{
		instance = this;
	}
}
