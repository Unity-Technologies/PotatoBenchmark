using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class SetupTests : MonoBehaviour
{
	public RenderPipelineAsset m_RenderPipeline;

	public void OnEnable()
	{
		GraphicsSettings.renderPipelineAsset = m_RenderPipeline;
		Application.targetFrameRate = 300;
	}
}
