using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(LegsPreset))]
public class LegsPresetEditor : BasePresetEditor
{
    private PresetContainer hips;
    private PresetContainer legLeft;
    private PresetContainer legRight;

    public override void OnEnable()
    {
        base.OnEnable();

        var mateParts = modularCharacters.transform.Find("Modular_Characters")?.Find("Male_Parts");

        hips = new PresetContainer(mateParts?.Find("Male_10_Hips"), "hips");
        legLeft = new PresetContainer(mateParts?.Find("Male_11_Leg_Right"), "legLeft");
        legRight = new PresetContainer(mateParts?.Find("Male_12_Leg_Left"), "legRight");

        SetPrefab(hips);
        SetPrefab(legLeft);
        SetPrefab(legRight);

        var buttonHips = new ButtonPopupList(hips.Name, new List<string>(hips.GetNames()), hips.GetIndex, (index) => SelectionChange(index, hips));
        var buttonLegLeft = new ButtonPopupList(legLeft.Name, new List<string>(legLeft.GetNames()), legLeft.GetIndex, (index) => SelectionChange(index, legLeft));
        var buttonLegRight = new ButtonPopupList(legRight.Name, new List<string>(legRight.GetNames()), legRight.GetIndex, (index) => SelectionChange(index, legRight));

        rootVisualElement.Add(buttonHips);
        rootVisualElement.Add(buttonLegLeft);
        rootVisualElement.Add(buttonLegRight);
    }

    public void OnDisable()
    {
        DestroyImmediate(hips.GetPreset());
        DestroyImmediate(legLeft.GetPreset());
        DestroyImmediate(legRight.GetPreset());
    }
}
