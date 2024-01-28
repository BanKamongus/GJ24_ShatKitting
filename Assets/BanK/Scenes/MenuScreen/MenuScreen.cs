using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScreen : MonoBehaviour
{
    public Animator fadeblack;
    public Animator swipeScreen;
    public float TransitionTime = 0.1f;
    public float SwipeSpeed = 10.0f;
    public float SwipeDistance = 10.0f;

    private int Screen = 0;

    public float holdDuration = 3.0f;
    private bool isHolding = false;
    private float holdStartTime;

    public Image Bar;
    public GameObject Text;


    void Start()
    {

        Bar.fillAmount = 0;

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }






        if (Screen == 0 && Input.GetKey(KeyCode.Space))
        {
            //StartCoroutine(SwipeText());

            swipeScreen.SetTrigger("Swipe");
            Screen = 1;
        }

        if (Screen == 1)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (!isHolding)
                {
                    StartHold();
                }
            }
            else
            {
                StopHold();
            }

            if (isHolding)
            {
                float fillAmount = Mathf.Clamp01((Time.time - holdStartTime) / holdDuration);
                Bar.fillAmount = fillAmount;

                if (Time.time - holdStartTime >= holdDuration)
                {

                    fadeblack.SetTrigger("Start");
                    //StopHold();
                    LoadNextScene();
                    
                }
            }
            else
            {
                // If not holding, smoothly decrease the fill amount
                float newFillAmount = Mathf.MoveTowards(Bar.fillAmount, 0.0f, Time.deltaTime / TransitionTime);
                Bar.fillAmount = newFillAmount;
            }

        }
    }

    void StartHold()
    {
        isHolding = true;
        holdStartTime = Time.time;
    }

    void StopHold()
    {
        isHolding = false;

    }

    IEnumerator SwipeText()
    {
        Vector3 startPosition = Text.transform.position;
        Vector3 targetPosition = new Vector3(900, Text.transform.position.y, Text.transform.position.z);

        float elapsedTime = 0f;

        while (elapsedTime < .5f)
        {
            float t = Mathf.Clamp01(elapsedTime / .5f);
            Text.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Text.transform.position = targetPosition;
    }

    public void LoadNextScene()
    {


        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadScene(int levelIndex)
    {

        yield return new WaitForSeconds(TransitionTime);
        SceneManager.LoadScene(levelIndex);

    }
}
