using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class CustomMenuPresetEditor
{
    public static CustomStage Stage { get; private set; }

    [MenuItem("-------------------->>>>>> Character Preset Editor <<<<<<--------------------/Head")]
    public static void Head() => InitStage("HeadPreset", typeof(HeadPreset));

    [MenuItem("-------------------->>>>>> Character Preset Editor <<<<<<--------------------/Torso")]
    public static void Torso() => InitStage("TorsoPreset", typeof(TorsoPreset));

    [MenuItem("-------------------->>>>>> Character Preset Editor <<<<<<--------------------/Legs")]
    public static void Legs() => InitStage("LegsPreset", typeof(LegsPreset));

    public static void Exit()
    {
        if (StageUtility.GetCurrentStage() != StageUtility.GetMainStage())
            StageUtility.GoToMainStage();
    }

    private static void InitStage(string presetName, System.Type presetType)
    {
        Exit();

        /**
        * Easy peasy lemon squeezy 😎
        */
        Stage = ScriptableObject.CreateInstance<CustomStage>();
        Stage.name = "Edit "+presetName;
        StageUtility.GoToStage(Stage, false);

        var preset = new GameObject(presetName, presetType);
        StageUtility.PlaceGameObjectInCurrentStage(preset);

        Selection.activeObject = preset;

        SceneView.lastActiveSceneView.sceneLighting = false;
        SceneView.lastActiveSceneView.sceneViewState.showSkybox = false;
        SceneView.RepaintAll();
    }
}
