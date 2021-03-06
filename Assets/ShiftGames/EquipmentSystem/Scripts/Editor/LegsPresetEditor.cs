using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(LegsPreset))]
public class LegsPresetEditor : BasePresetEditor
{
    private PresetContainer hips;
    private PresetContainer legLeft;
    private PresetContainer legRight;

    public override string GetSaveFolderName() => "Legs";

    public override void Init()
    {
        var mateParts = modularCharacters.transform.Find("Modular_Characters")?.Find("Male_Parts");

        hips = new PresetContainer(mateParts?.Find("Male_10_Hips"), "Hips");
        legLeft = new PresetContainer(mateParts?.Find("Male_11_Leg_Right"), "Leg right");
        legRight = new PresetContainer(mateParts?.Find("Male_12_Leg_Left"), "Leg left");

        SetPrefab(hips);
        SetPrefab(legLeft);
        SetPrefab(legRight);

        var buttonHips = new ButtonPopupList(hips.Name, new List<string>(hips.GetNames()), hips.GetIndex, (index) => SelectionChange(index, hips));
        var buttonLegLeft = new ButtonPopupList(legLeft.Name, new List<string>(legLeft.GetNames()), legLeft.GetIndex, (index) => SelectionChange(index, legLeft));
        var buttonLegRight = new ButtonPopupList(legRight.Name, new List<string>(legRight.GetNames()), legRight.GetIndex, (index) => SelectionChange(index, legRight));

        bodyVisualElement.Add(buttonHips);
        bodyVisualElement.Add(buttonLegLeft);
        bodyVisualElement.Add(buttonLegRight);
    }

    public override void Destroy()
    {
        DestroyImmediate(hips.GetPreset());
        DestroyImmediate(legLeft.GetPreset());
        DestroyImmediate(legRight.GetPreset());
    }
}
