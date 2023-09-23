using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CustomRenderPipeline : RenderPipeline
{
    private CameraRenderer _renderer = new CameraRenderer();
    bool useDynamicBatching, useGPUInstancing;
    private ShadowSettings shadowSettings;
    
    public CustomRenderPipeline (
        bool useDynamicBatching, bool useGPUInstancing, bool useSRPBatcher, 
        ShadowSettings shadowSettings
    )
    {
        this.shadowSettings = shadowSettings;
        this.useDynamicBatching = useDynamicBatching;
        this.useGPUInstancing = useGPUInstancing;
        GraphicsSettings.useScriptableRenderPipelineBatching = useSRPBatcher;
        GraphicsSettings.lightsUseLinearIntensity = true;
    }
    
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        
    }

    // because the camera array parameter requires allocating memory every frame an
    // alternative has been introduced that has a list parameter instead.
    protected override void Render(ScriptableRenderContext context, List<Camera> cameras)
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            _renderer.Render(
                context, cameras[i], useDynamicBatching, useGPUInstancing,
                shadowSettings
            );
        }
    }
}
