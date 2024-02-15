using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsController : MonoBehaviour
{
    [SerializeField]
    private GameObject _consentUI;
    async void Start()
    {
        await UnityServices.InitializeAsync();

        AskForConsent();
    }

    void AskForConsent()
    {
        if (!PlayerPrefs.HasKey("AnalyticsConsent"))
        {
            _consentUI.SetActive(true);
            return;
        }
        int consent = PlayerPrefs.GetInt("AnalyticsConsent");

        if (consent == 1)
            ConsentGiven();
        else
            ConsentDenied();
    }

    public void ConsentGiven()
    {
        AnalyticsService.Instance.StartDataCollection();
        _consentUI.SetActive(false);
        PlayerPrefs.SetInt("AnalyticsConsent", 1);
    }
    public void ConsentDenied()
    {
        _consentUI.SetActive(false);
        PlayerPrefs.SetInt("AnalyticsConsent", 0);
    }
}
