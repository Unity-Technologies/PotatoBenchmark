using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.LWRP;

[ExecuteInEditMode]
public class ConfigureShadowCascades : MonoBehaviour
{
    public bool m_EnableCascades;

    public void Awake()
    {
        UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset asset = GraphicsSettings.renderPipelineAsset as UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset;
        if (asset != null)
            asset.shadowCascadeOption = (m_EnableCascades) ? UnityEngine.Rendering.Universal.ShadowCascadesOption.FourCascades : UnityEngine.Rendering.Universal.ShadowCascadesOption.NoCascades;
        QualitySettings.shadowCascades = (m_EnableCascades) ? 4 : 1;
    }
}
