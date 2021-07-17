using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class CustomMenuPresetEditor
{
    [MenuItem("-------------------->>>>>> Character Preset Editor <<<<<<--------------------/Head")]
    public static void Head() => InitStage("HeadPreset", typeof(HeadPreset));

    [MenuItem("-------------------->>>>>> Character Preset Editor <<<<<<--------------------/Torso")]
    public static void Torso() => InitStage("TorsoPreset", typeof(TorsoPreset));

    [MenuItem("-------------------->>>>>> Character Preset Editor <<<<<<--------------------/Legs")]
    public static void Legs() => InitStage("LegsPreset", typeof(LegsPreset));

    private static void InitStage(string presetName, System.Type presetType)
    {
        if (StageUtility.GetCurrentStage() != StageUtility.GetMainStage())
            StageUtility.GoToMainStage();

        /**
        * Easy peasy lemon squeezy 😎
        */
        var stage = ScriptableObject.CreateInstance<CustomStage>();
        stage.name = "Edit "+presetName;
        StageUtility.GoToStage(stage, false);

        var preset = new GameObject(presetName, presetType);
        StageUtility.PlaceGameObjectInCurrentStage(preset);

        Selection.activeObject = preset;

        SceneView.lastActiveSceneView.sceneLighting = false;
        SceneView.lastActiveSceneView.sceneViewState.showSkybox = false;
        SceneView.RepaintAll();
    }
}
