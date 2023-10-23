using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ChannelSelector : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI channelText;
    [SerializeField] private Image switchImage;
    [SerializeField] private AudioSource source;

    [Header("InactivityTimerVariables")]
    [SerializeField] private float timeInactive;
    [SerializeField] private float timeUntilFade = 10f;

    [Header("Channel Variables")]
    [SerializeField] private int[] channelFields = new int[3];
    [SerializeField] private int activeField;
    [SerializeField] private string channelFullField;

    [Header("AudioClips")]
    [SerializeField] private AudioClip staticClip;
    [SerializeField] private AudioClip buttonClip;

    [Header("Text Colors")]
    [SerializeField] private Color colorStandard;
    [SerializeField] private Color colorEntered;
    [SerializeField] private Color colorFaded;

    private KeyCode[] numericKeys =
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9
    };

    private bool canInput;

    void Start()
    {
        channelText.text = SceneManager.GetActiveScene().name;
        switchImage.enabled = false;

        timeInactive = 0f;
        activeField = 0;
        canInput = true;
    }

    void Update()
    {
        for (int i = 0; i < numericKeys.Length; i++)
        {
            if (Input.GetKeyDown(numericKeys[i]) && canInput)
            {
                StopAllCoroutines();

                timeInactive = 0f;
                channelText.color = colorStandard;

                int numberPressed = i + 1;
                Debug.Log(numberPressed);

                channelFields[activeField] = numberPressed;
                activeField += 1;

                channelFullField = channelFields[2].ToString() + channelFields[1].ToString() + channelFields[0].ToString();
                channelText.text = channelFullField;

                source.PlayOneShot(buttonClip, Random.Range(.1f,1f));
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(LoadChannel());
        }

        if (timeInactive >= timeUntilFade)
        {
            FlushChannel(false);

            StartCoroutine(FadeText());

            return;
        }

        timeInactive += Time.deltaTime;
    }

    private void FlushChannel(bool isNull)
    {
        if (isNull)
            channelText.text = "NULL";

        channelFields[0] = 0;
        channelFields[1] = 0;
        channelFields[2] = 0;
        channelFullField = "000";
        activeField = 0;
    }

    IEnumerator LoadChannel()
    {
        channelText.color = colorEntered;
        switchImage.enabled = true;
        source.PlayOneShot(staticClip);
        canInput = false;

        yield return new WaitForSeconds(.5f);

        SceneManager.LoadScene(channelFullField);
        
        yield return new WaitForSeconds(.5f);

        canInput = true;
        switchImage.enabled = false;
        FlushChannel(true);
    }

    IEnumerator FadeText()
    {
        colorFaded.a = 1f;
        float transparencyStep = .01f;

        channelText.text = SceneManager.GetActiveScene().name;

        while (true)
        {
            if (colorFaded.a > 0f)
                colorFaded.a -= transparencyStep;

            channelText.color = colorFaded;

            yield return null;
        }
    }
}
