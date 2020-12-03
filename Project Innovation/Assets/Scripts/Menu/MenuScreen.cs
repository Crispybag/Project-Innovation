using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : MonoBehaviour
{
    public enum MENUSELECT
    {
        START = 0,
        AMBVOLUME = 1,
        VOIVOLUME = 2,
        EXIT = 3
    }

    private MENUSELECT menuSelect = MENUSELECT.START;
    private SwipeControls _swipeControls;
    private TapScreen _tapScreen;
    private MenuSounds _menuSounds;
    private bool optionChanged = false;
    private void Start()
    {
       
        _swipeControls = GetComponent<SwipeControls>();
        _tapScreen = GetComponent<TapScreen>();
        _menuSounds = GetComponent<MenuSounds>();
        //_menuSounds.playSound(0);
    }

    private void Update()
    {
        optionChanged = false;
        if (_swipeControls.direction == SwipeControls.DIRECTION.LEFT || _swipeControls.direction == SwipeControls.DIRECTION.RIGHT || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            selectMenuOption(_swipeControls.direction);
        }

        if(optionChanged)
        {
            playSwitchSound(menuSelect);
        }

        if (_tapScreen.isTapping || Input.GetMouseButtonDown(0)) onTap(menuSelect);

    }

    private void selectMenuOption(SwipeControls.DIRECTION direction)
    {
        //A check whether the option has changed or not
        optionChanged = true;

        if (direction == SwipeControls.DIRECTION.LEFT || Input.GetKeyDown(KeyCode.A))
        {
            if ((int)menuSelect == 0) menuSelect = MENUSELECT.EXIT;
            else menuSelect--;
        }

        else if (direction == SwipeControls.DIRECTION.RIGHT || Input.GetKeyDown(KeyCode.D))
        {
            if ((int)menuSelect == 3) menuSelect = MENUSELECT.START;
            else menuSelect++;
        }
    }

    private void playSwitchSound(MENUSELECT direction)
    {
        _menuSounds.playSound((int)direction + 1);
    }

    private void onTap(MENUSELECT menu)
    {

        if (menu == MENUSELECT.START)
        {
            _menuSounds.stopSound();
            goToGame();

        }

        else if (menu == MENUSELECT.EXIT)
        {
            _menuSounds.stopSound();
            exitGame();
        }
    }



    private void goToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    private void exitGame()
    {
        Application.Quit();
    }

}
