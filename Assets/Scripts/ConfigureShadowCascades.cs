using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.LWRP;

[ExecuteInEditMode]
public class ConfigureShadowCascades : MonoBehaviour
{
    public bool m_EnableCascades;

    public void Awake()
    {
        LightweightRenderPipelineAsset asset = GraphicsSettings.renderPipelineAsset as LightweightRenderPipelineAsset;
        if (asset != null)
            asset.shadowCascadeOption = (m_EnableCascades) ? ShadowCascadesOption.FourCascades : ShadowCascadesOption.NoCascades;
        QualitySettings.shadowCascades = (m_EnableCascades) ? 4 : 1;
    }
}
