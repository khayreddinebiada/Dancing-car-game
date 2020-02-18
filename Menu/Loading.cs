using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public Text textExpired;
    public Slider loadingSlider;

    void Start()
    {
        StartCoroutine(waitAndStart());
    }
    IEnumerator waitAndStart()
    {
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(LoadAsynchronously("Menu"));
    }

    IEnumerator LoadAsynchronously(string scenceName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scenceName);

        while (!operation.isDone)
        {

            loadingSlider.value = operation.progress;
            yield return null;

        }

    }

}
