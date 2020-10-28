using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Transition _transition;

    public void TransitionToGame()
    {
        _transition.RunExit(LoadGame);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Sandbox");
    }

    public void TransitionToHome()
    {
        _transition.RunExit(LoadHome);
    }

    private void LoadHome()
    {
        SceneManager.LoadScene("Main");
    }
}
