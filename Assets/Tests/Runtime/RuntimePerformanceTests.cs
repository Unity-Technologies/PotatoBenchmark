using System.Collections;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.LWRP;
using UnityEngine.TestTools;

public class RuntimePerformanceTests
{
    private static readonly int warmupFrames = 10;
    private static readonly int measureFrames = 200;
    public const string version = "0.3.0";

    public static readonly string[] sceneNames = 
    {   
        "01 - FillRate (Lit Shader - Directional Light)",
        "02 - FillRate (Lit Shader - Multiple Lights)",
        "03 - Realtime Shadows",
        "04 - Realtime Shadows Cascades",
        "05 - Batching",
        "07 - Bandwidth",
        "08 - FillRate (Terrain)",
    };

    enum PipelineDefine
    {
        LWRP,
        Builtin,
    };

    [UnityTest, Performance]
    [Version(version)]
    public IEnumerator _001_FillRate_LitShader_DirectionalLight()
    {
        yield return RunTestScene(PipelineDefine.LWRP, 0);
    }

    [UnityTest, Performance]
    [Version(version)]
    public IEnumerator _002_FillRate_LitShader_MultipleLights()
    {
        yield return RunTestScene(PipelineDefine.LWRP, 1);
    }

    [UnityTest, Performance]
    [Version(version)]
    public IEnumerator _003_RealtimeShadows()
    {
        yield return RunTestScene(PipelineDefine.LWRP, 2);
    }

    [UnityTest, Performance]
    [Version(version)]
    public IEnumerator _004_RealtimeCascadedShadows()
    {
        yield return RunTestScene(PipelineDefine.LWRP, 3);
    }

    [UnityTest, Performance]
    [Version(version)]
    public IEnumerator _005_Batching()
    {
        yield return RunTestScene(PipelineDefine.LWRP, 4);
    }
    
    [UnityTest, Performance]
    [Version(version)]
    public IEnumerator _007_Bandwidth()
    {
        yield return RunTestScene(PipelineDefine.LWRP, 5);
    }
    
    [UnityTest, Performance]
    [Version(version)]
    public IEnumerator _008_FillRate_Terrain()
    {
        yield return RunTestScene(PipelineDefine.LWRP, 6);
    }

    [UnityTest, Performance]
    [Version(version)]
    public IEnumerator _101_FillRate_LitShader_DirectionalLight()
    {
        yield return RunTestScene(PipelineDefine.Builtin, 0);
    }

    [UnityTest, Performance]
    [Version(version)]
    public IEnumerator _102_FillRate_LitShader_MultipleLights()
    {
        yield return RunTestScene(PipelineDefine.Builtin, 1);
    }

    [UnityTest, Performance]
    [Version(version)]
    public IEnumerator _103_RealtimeShadows()
    {
        yield return RunTestScene(PipelineDefine.Builtin, 2);
    }

    [UnityTest, Performance]
    [Version(version)]
    public IEnumerator _104_RealtimeCascadedShadows()
    {
        yield return RunTestScene(PipelineDefine.Builtin, 3);
    }

    [UnityTest, Performance]
    [Version(version)]
    public IEnumerator _105_Batching()
    {
        yield return RunTestScene(PipelineDefine.Builtin, 4);
    }
    
    [UnityTest, Performance]
    [Version(version)]
    public IEnumerator _107_Bandwidth()
    {
        yield return RunTestScene(PipelineDefine.Builtin, 5);
    }
    
    [UnityTest, Performance]
    [Version(version)]
    public IEnumerator _108_FillRate_Terrain()
    {
        yield return RunTestScene(PipelineDefine.Builtin, 6);
    }

    IEnumerator RunTestScene(PipelineDefine pipeline, int testIndex)
    {
        string sceneName = string.Format("{0}{1}"   , (int)pipeline, sceneNames[testIndex]);
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        yield return Measure.Frames().WarmupCount(warmupFrames).MeasurementCount(measureFrames).Run();
    }
}
