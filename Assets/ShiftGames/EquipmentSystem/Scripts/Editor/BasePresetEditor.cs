using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public abstract class BasePresetEditor : Editor
{
    protected Material material;
    protected GameObject modularCharacters;
    protected VisualElement rootVisualElement;

    private TextField prefabName;

    private FloatField armor;
    private FloatField strength;
    private FloatField agility;
    private FloatField maxSpeed;
    private FloatField convenience;

    public abstract void CreateInspector(ref VisualElement visualElement);
    public abstract string GetSaveFolderName();

    public override VisualElement CreateInspectorGUI() => rootVisualElement;

    public void OnEnable()
    {
        if (StageUtility.GetCurrentStage() == StageUtility.GetMainStage())
            return;

        material = (Material)AssetDatabase.LoadAssetAtPath("Assets/PolygonFantasyHeroCharacters/Materials/FantasyHero.mat", typeof(Material));
        modularCharacters = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/PolygonFantasyHeroCharacters/Models/ModularCharacters.fbx", typeof(GameObject));
        rootVisualElement = new VisualElement();

        var visualElement = new VisualElement();
        CreateInspector(ref visualElement);

        if (visualElement != null)
            rootVisualElement.Add(visualElement);

        CreateStatsFields();

        rootVisualElement.Add(new Label("Save"));
        prefabName = new TextField("Prefab name");
        rootVisualElement.Add(prefabName);
        var saveButton = new Button(() => SavePrefab(GetSaveFolderName(), GetPrefabName())) { text = "Save Prefab" };
        rootVisualElement.Add(saveButton);
    }

    protected void SetPrefab(PresetContainer presetContainer)
    {
        if (presetContainer.GetPreset() != null)
            DestroyImmediate(presetContainer.GetPreset());

        var preset = Instantiate(presetContainer.GetVariant(), (target as Component).transform);
        if (preset != null)
        {
            preset.name = presetContainer.Name;
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

    protected void SavePrefab(string folderName, string prefabName)
    {
        var prefab = (target as Component).gameObject;
        var stats = prefab.GetComponent<BasePreset>();
        stats.armor = armor.value;
        stats.strength = strength.value;
        stats.agility = agility.value;
        stats.maxSpeed = maxSpeed.value;
        stats.convenience = convenience.value;

        if (prefabName != null && prefabName != "")
            PrefabUtility.SaveAsPrefabAsset(prefab, $"Assets/ShiftGames/EquipmentSystem/Presets/{folderName}/{prefabName}.prefab");
    }

    private string GetPrefabName() => prefabName.value;

    private void CreateStatsFields()
    {
        armor = new FloatField("Armor");
        strength = new FloatField("Strength");
        agility = new FloatField("Agility");
        maxSpeed = new FloatField("Max speed");
        convenience = new FloatField("Convenience");

        rootVisualElement.Add(new Label("Stats"));
        rootVisualElement.Add(armor);
        rootVisualElement.Add(strength);
        rootVisualElement.Add(agility);
        rootVisualElement.Add(maxSpeed);
        rootVisualElement.Add(convenience);
    }
}