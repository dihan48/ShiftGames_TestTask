using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;

public abstract class BasePresetEditor : Editor
{
    protected Material material;
    protected GameObject modularCharacters;
    protected VisualElement rootVisualElement;

    public virtual void OnEnable()
    {
        material = (Material)AssetDatabase.LoadAssetAtPath("Assets/PolygonFantasyHeroCharacters/Materials/FantasyHero.mat", typeof(Material));
        modularCharacters = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/PolygonFantasyHeroCharacters/Models/ModularCharacters.fbx", typeof(GameObject));
        rootVisualElement = new VisualElement();
    }

    public override VisualElement CreateInspectorGUI() => rootVisualElement;

    protected void SetPrefab(PresetContainer presetContainer)
    {
        if (presetContainer.GetPreset() != null)
            DestroyImmediate(presetContainer.GetPreset());

        var preset = Instantiate(presetContainer.GetVariant());
        if (preset != null)
        {
            presetContainer.SetPreset(preset);
            var rend = preset.GetComponent<SkinnedMeshRenderer>();
            if (rend != null)
                rend.material = material;

            StageUtility.PlaceGameObjectInCurrentStage(preset);
        }
    }

    protected void SelectionChange(int index, PresetContainer presetContainer)
    {
        if (presetContainer.IsValid && index != presetContainer.GetIndex())
        {
            presetContainer.SetIndex(index);
            SetPrefab(presetContainer);
        }
    }
}