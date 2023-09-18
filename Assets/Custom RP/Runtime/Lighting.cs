using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Lighting
{
    private const string bufferName = "Lighting";

    private static int
        dirLightColorId = Shader.PropertyToID("_DirectionalLightColor"),
        dirLightDirectionId = Shader.PropertyToID("_DirectionalLightDirection");

    private CommandBuffer buffer = new CommandBuffer()
    {
        name = bufferName
    };

    public void Setup(ScriptableRenderContext context)
    {
        buffer.BeginSample(bufferName);
        SetupDirectionalLight();
        buffer.EndSample(bufferName);
        context.ExecuteCommandBuffer(buffer);
        buffer.Clear();
    }

    void SetupDirectionalLight()
    {
        Light light = RenderSettings.sun;
        buffer.SetGlobalVector(
            dirLightColorId, light.color.linear * light.intensity
        );
        buffer.SetGlobalVector(dirLightDirectionId, - light.transform.forward);
    }
}
