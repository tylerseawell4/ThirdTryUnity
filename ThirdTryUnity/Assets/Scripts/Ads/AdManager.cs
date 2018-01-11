using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

[RequireComponent(typeof(Button))]
public class AdManager : MonoBehaviour
{
    Button m_Button;
    public string placementId = "rewardedVideo";

#if UNITY_IOS
   private string    _gameID = "1666894";
#elif UNITY_ANDROID
    private string _gameID = "1666893";
#elif UNITY_EDITOR
    private string _gameID = "1666893";
#endif

    void Awake()
    {
        m_Button = GetComponent<Button>();
        if (m_Button) m_Button.onClick.AddListener(ShowAd);

        if (Advertisement.isSupported)
            Advertisement.Initialize(_gameID, true);
    }

    public void ShowAd()
    {
#if UNITY_EDITOR
        StartCoroutine(WaitForAd());
#endif

        ShowOptions options = new ShowOptions();
        options.resultCallback = AdCallbackhandler;

        Advertisement.Show(placementId, options);
    }

    private void Update()
    {
        if (m_Button)
            m_Button.interactable = Advertisement.IsReady(placementId);
    }

    void AdCallbackhandler(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Ad Finished. Rewarding player...");
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad skipped. Son, I am dissapointed in you");
                break;
            case ShowResult.Failed:
                Debug.Log("I swear this has never happened to me before");
                break;
        }
    }

    IEnumerator WaitForAd()
    {
        float currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return null;

        while (Advertisement.isShowing)
            yield return null;

        Time.timeScale = currentTimeScale;
    }
}