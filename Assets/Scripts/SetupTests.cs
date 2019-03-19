using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class SetupTests : MonoBehaviour
{
	public RenderPipelineAsset m_RenderPipeline;

	public void OnEnable()
	{
		Debug.Log("Executed");
		GraphicsSettings.renderPipelineAsset = m_RenderPipeline;
		Application.targetFrameRate = 300;
	}
}
