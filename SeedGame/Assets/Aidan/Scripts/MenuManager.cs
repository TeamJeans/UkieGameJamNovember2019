using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	public static MenuManager Instance { get; private set; }

	enum MenuState
	{
		NO_MENU,
		MAIN_MENU,
		PAUSE_MENU,
		VICTORY_MENU
	}
	[SerializeField] private MenuState menuState = MenuState.NO_MENU;
	[SerializeField] private GameObject pauseMenuObject = null;
	[SerializeField] private GameObject mainMenuObject = null;
	[SerializeField] private GameObject victoryMenuObject = null;

	private void Awake()
	{
		// Makes sure that this is the only instance of this object in the scene
		if (Instance != null)
		{
			if (Instance != this)
			{
				Destroy(this.gameObject);
			}
		}
		else
		{
			Instance = this;
		}

		// Main menu should be enabled initialy
		mainMenuObject.SetActive(true);
		mainMenuObject.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
		Time.timeScale = 0;

		// Pause menu should be disabled initialy
		pauseMenuObject.SetActive(false);
		pauseMenuObject.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;

		// Victory menu should be disabled initialy
		victoryMenuObject.SetActive(false);
		victoryMenuObject.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
	}

	private void Update()
	{
		// Update depending on the state
		switch (menuState)
		{
			case MenuState.NO_MENU: UpdateNoMenuState();
				break;
			case MenuState.MAIN_MENU: UpdateMainMenuState();
				break;
			case MenuState.PAUSE_MENU: UpdatePauseMenuState();
				break;
			case MenuState.VICTORY_MENU: UpdateVictoryMenuState();
				break;
			default:
				break;
		}
	}

	private void UpdateNoMenuState()
	{
		//Check if escape has been pressed. if so change to the pause state
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			menuState = MenuState.PAUSE_MENU;
			pauseMenuObject.SetActive(true);
			pauseMenuObject.GetComponent<Animator>().SetTrigger("FadeIn");
			Time.timeScale = 0;
		}
	}

	private void UpdateMainMenuState()
	{
		// Check if the left mouse button has been pressed, then start the game
		if (Input.GetMouseButtonDown(0))
		{
			menuState = MenuState.NO_MENU;
			//mainMenuObject.SetActive(false);
			mainMenuObject.GetComponent<Animator>().SetTrigger("FadeOut");
			Time.timeScale = 1;
		}
	}

	private void UpdatePauseMenuState()
	{
		// If the escape key has been pressed, disable the pause menu and go back to the no menu state
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			menuState = MenuState.NO_MENU;
			//pauseMenuObject.SetActive(false);
			pauseMenuObject.GetComponent<Animator>().SetTrigger("FadeOut");
			Time.timeScale = 1;
		}
	}

	private void UpdateVictoryMenuState()
	{
		// If the player presses R reset the game
		if (Input.GetKeyDown(KeyCode.R))
		{
			menuState = MenuState.NO_MENU;
			victoryMenuObject.GetComponent<Animator>().SetTrigger("FadeOut");
			Time.timeScale = 1;

			GameManager.Instance.ResetGame();
		}
	}

	public void EndReached()
	{
		victoryMenuObject.SetActive(true);
		menuState = MenuState.VICTORY_MENU;
		victoryMenuObject.GetComponent<Animator>().SetTrigger("FadeIn");
		Time.timeScale = 0;
	}
}
