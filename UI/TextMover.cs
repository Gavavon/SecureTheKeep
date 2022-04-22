using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMover : MonoBehaviour
{
	[HideInInspector]
	public bool canMoveText = true;

	public GateMechanics gate1;
	public GateMechanics gate2;

	public ObjectMovement textBox;
	public Text messageText;
	string[] tutorialTexts;
	private int messageNum = 0;
	private TextWriter.TextWriterSingle textWriterSingle;

	private void Awake()
	{
		tutorialTexts = TutorialManager.instance.tutorialTexts;
	}
	public void printText(int i)
	{
		if (textWriterSingle != null && textWriterSingle.IsActive())
		{
			textWriterSingle.WriteAllAndDestory();
		}
		else
		{
			textWriterSingle = TextWriter.instance.AddWriter(messageText, tutorialTexts[i], true);
		}
	}
	public void progressText() 
	{

		if (messageNum != tutorialTexts.Length)
		{
			textBox.showgameObj = true;
			printText(messageNum);
		}
		else 
		{
			textBox.showgameObj = false;
			canMoveText = false;
			gate1.healGate();
			gate2.healGate();
			WaveManager.instance.createWave();
		}
		if (messageNum == 1) 
		{
			StartCoroutine(WaveManager.instance.spawnEnemy(1, true, 0));
		}
		messageNum++;
	}
}
