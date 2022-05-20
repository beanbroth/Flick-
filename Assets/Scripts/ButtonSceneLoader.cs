using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSceneLoader : MonoBehaviour
{
    public void AskGameManagerToLoadMainMenu()
    {
        GameManager.Instance.LoadSpecificLevel(0);
    }

    public void AskGameManagerToLoadFirstLevel()
    {
        GameManager.Instance.LoadSpecificLevel(1);
    }
}
