using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip startClip;
    [SerializeField] private AudioClip buttonClip;

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject mainMenuBG;

    [SerializeField] private GameObject optionsMenu;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        StartCoroutine(StartGameSequence());
    }

    public void Options()
    {
        source.PlayOneShot(buttonClip);
        optionsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameSequence());
    }

    public void ReturnMain()
    {
        source.PlayOneShot(buttonClip);
        optionsMenu.SetActive(false);
    }

    private IEnumerator StartGameSequence()
    {
        source.PlayOneShot(buttonClip);
        yield return new WaitForSeconds(.2f);

        optionsButton.SetActive(false);
        quitButton.SetActive(false);
        mainMenuBG.SetActive(false);

        source.PlayOneShot(startClip);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("001");
    }

    private IEnumerator QuitGameSequence()
    {
        source.PlayOneShot(buttonClip);
        yield return new WaitForSeconds(.2f);

        optionsButton.SetActive(false);
        startButton.SetActive(false);
        mainMenuBG.SetActive(false);

        source.PlayOneShot(startClip);

        yield return new WaitForSeconds(1f);

        Application.Quit();
    }
}
