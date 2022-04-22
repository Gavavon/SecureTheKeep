using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{

	public static bool playTutorial = true;
	public enum gameplayState
	{
		UIMenu,
		Gameplay
	}
	public static gameplayState currentGameState;

	public static GameManagement instance;
	private void Awake()
	{
		instance = this;
		DontDestroyOnLoad(gameObject);
		setGameState(gameplayState.UIMenu);
		Application.targetFrameRate = 60;
	}
	public void setplayTutorial(bool play) 
	{
		playTutorial = play;
	}
	public bool getplayTutorial()
	{
		return playTutorial;
	}
	public void setGameState(gameplayState i)
	{
		currentGameState = i;
	}
	public gameplayState getGameState()
	{
		return currentGameState;
	}
	public void updateFPS(int rate)
	{
		Application.targetFrameRate = rate;
	}
	public void updateTutorial(bool play)
	{
		setplayTutorial(play);
	}
	public void loadMainGame() 
	{
		SceneManager.LoadScene(1);
		AudioManager.instance.setMusic();
		Cursor.lockState = CursorLockMode.Locked;
		currentGameState = gameplayState.Gameplay;
	}

	public void loadMainMenu()
	{
		SceneManager.LoadScene(0);
		AudioManager.instance.setMusic();
		Cursor.lockState = CursorLockMode.None;
		currentGameState = gameplayState.UIMenu;
	}
}
