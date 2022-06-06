using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSceneLoader : MonoBehaviour
{

    bool _wasPressed = false;
    public void AskGameManagerToLoadMainMenu()
    {
        if (_wasPressed)
        {
            return;
        }

        _wasPressed = true;
        GameManager.Instance.LoadSpecificLevel(0);
    }

    public void AskGameManagerToLoadFirstLevel()
    {
        if (_wasPressed)
        {
            return;
        }

        _wasPressed = true;
        GameManager.Instance.LoadSpecificLevel(1);
    }
}
