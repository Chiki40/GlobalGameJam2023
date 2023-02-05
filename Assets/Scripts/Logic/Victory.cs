using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Victory : MonoBehaviour
{
    [SerializeField]
    private Image _ruth = null;
    [SerializeField]
    private Image _bastardo = null;
    [SerializeField]
    private Image _title = null;

    private IEnumerator Start()
	{
        _ruth.color = Color.clear;
        _bastardo.color = Color.clear;
        _title.color = Color.clear;

        yield return new WaitForSeconds(2.0f);
        float time = 0.0f;
        while (time < 2.0f)
		{
            time += Time.deltaTime;
            _ruth.color = Color.Lerp(Color.clear, Color.white, time / 2.0f);
            yield return null;
		}

        yield return new WaitForSeconds(5.0f);

        time = 0.0f;
        while (time < 2.0f)
        {
            time += Time.deltaTime;
            _bastardo.color = Color.Lerp(Color.clear, Color.white, time / 2.0f);
            yield return null;
        }

        yield return new WaitForSeconds(8.0f);

        time = 0.0f;
        while (time < 6.0f)
        {
            time += Time.deltaTime;
            _ruth.color = Color.Lerp(Color.white, Color.clear, time / 6.0f);
            yield return null;
        }

        time = 0.0f;
        while (time < 2.0f)
        {
            time += Time.deltaTime;
            _bastardo.color = Color.Lerp(Color.white, Color.clear, time / 2.0f);
            yield return null;
        }

        yield return new WaitForSeconds(3.0f);

        time = 0.0f;
        while (time < 5.0f)
        {
            time += Time.deltaTime;
            _title.color = Color.Lerp(Color.clear, Color.white, time / 5.0f);
            yield return null;
        }

        yield return new WaitForSeconds(8.0f);

        SceneManager.LoadScene("MainMenu");
    }
}
