using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class RunTests : MonoBehaviour
{
    public RenderPipelineAsset m_RenderPipeline;
    public float m_MeasureSeconds = 10.0f;
    public float m_SkipSeconds = 1.0f;

    public void Awake()
    {
        Application.targetFrameRate = 300;
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator MeasureFrames(string sceneName, float duration)
    {
        float average = 0.0f;
        float median = 0.0f;
        float min = 0.0f;
        float max = 0.0f;
        List<float> samples = new List<float>((int)(duration / 0.01666f));

        float elapsed = 0.0f;
        int frames = 0;

        duration *= 1000.0f;
        while (elapsed < duration)
        {
            float dt = Time.deltaTime * 1000.0f;
            samples.Add(dt);
            elapsed += dt;
            frames++;
            yield return null;
        }

        average = elapsed / frames;
        samples.Sort();
        float[] msArray = samples.ToArray();
        median = msArray[msArray.Length / 2];
        min = msArray[0];
        max = msArray[msArray.Length - 1];

        WriteResults(sceneName, average, median, min, max);
    }

    void WriteResults(string sceneName, float average, float median, float min, float max)
    {
        Debug.Log(string.Format("[Benchmark] {0}", sceneName));
        Debug.Log(string.Format("[Benchmark] AVG: {0} | MEDIAN {1} | MIN {2} | MAX {3}", average, median, min, max));
    }

    public void RunLWRP()
    {
        int testCount = (SceneManager.sceneCountInBuildSettings - 1) / 2;
        GraphicsSettings.renderPipelineAsset = m_RenderPipeline;
        QualitySettings.antiAliasing = 4;
        StartCoroutine(RunTestScenes(1, testCount + 1, m_MeasureSeconds, m_SkipSeconds));
    }

    public void RunBuiltin()
    {
        int testEnd = SceneManager.sceneCountInBuildSettings;
        int testStart = (testEnd - 1) / 2;
        GraphicsSettings.renderPipelineAsset = null;
        QualitySettings.antiAliasing = 4;
        StartCoroutine(RunTestScenes(testStart + 1, testEnd, m_MeasureSeconds, m_SkipSeconds));
    }

    IEnumerator RunTestScenes(int sceneIndexStart, int sceneIndexEnd, float testDuration, float skipSeconds)
    {
        for (int i = sceneIndexStart; i < sceneIndexEnd; ++i)
        {
            yield return SceneManager.LoadSceneAsync(i, LoadSceneMode.Single);
            yield return new WaitForSeconds(skipSeconds);
            string sceneName = SceneManager.GetSceneByBuildIndex(i).name;
            yield return MeasureFrames(sceneName, testDuration);
        }

        SceneManager.LoadSceneAsync(0);
        Destroy(gameObject);
    }

}
