using UnityEngine;

public class SkyboxBlender : MonoBehaviour
{
    public Material initialSkybox;
    public Material targetSkybox;
    public float blendDuration = 5f;

    private float transitionDuration = 20f; // 30秒で完全な夜に遷移
    private float initialExposure = 1.0f; // 初期の露出設定
    private float targetExposure = 0.2f; // 夜の露出設定
    private float currentTime = 0f;


    void Start()
    {
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime < transitionDuration)
        {
            float currentExposure = Mathf.Lerp(initialExposure, targetExposure, currentTime / transitionDuration);
            RenderSettings.skybox.SetFloat("_Exposure", currentExposure);
            DynamicGI.UpdateEnvironment(); // 環境のライティングを更新
        }
        else if(currentTime < 31)
        {
            //RenderSettings.skybox.SetFloat("_Exposure", 0);
            RenderSettings.skybox = targetSkybox;
        }
    }
}