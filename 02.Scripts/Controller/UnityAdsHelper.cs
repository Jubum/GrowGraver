using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsHelper : MonoBehaviour
{
    private const string android_game_id = "1568041";
    private const string ios_game_id = "1568042";

    private const string rewarded_video_id = "rewardedVideo";

    public GraverController graverController;
    public UIController uiController;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
#if UNITY_ANDROID
        Advertisement.Initialize(android_game_id);
#elif UNITY_IOS
        Advertisement.Initialize(ios_game_id);
#endif
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady(rewarded_video_id))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };

            Advertisement.Show(rewarded_video_id, options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                {
                    uiController.OnWarningPopUp("감사합니다. \n다이아 3개가 지급되었습니다.");
                    graverController.graver.Diamond += 3;
                    // 광고 시청이 완료되었을 때 처리

                    break;
                }
            case ShowResult.Skipped:
                {

                    uiController.OnWarningPopUp("광고가 스킵되었습니다.");
                    // 광고가 스킵되었을 때 처리

                    break;
                }
            case ShowResult.Failed:
                {
                    uiController.OnWarningPopUp("오류로 인해 광고 시청에 실패했습니다.");
                    // 광고 시청에 실패했을 때 처리

                    break;
                }
        }
    }
}