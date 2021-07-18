/*
 * Warning!
 * True experienced programmers may start burning their asses when viewing this shit code.
 * The author of this shit code understands perfectly well that it is possible to write better, but he did not have time.
 * Please accept this as it is and the author is very sorry
 */

using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(TorsoPreset))]
public class TorsoPresetEditor : BasePresetEditor
{
    private PresetContainer torso;
    private PresetContainer apperArmLeft;
    private PresetContainer apperArmRight;
    private PresetContainer lowerArmLeft;
    private PresetContainer lowerArmRight;
    private PresetContainer handLeft;
    private PresetContainer handRight;

    public override string GetSaveFolderName() => "Torso";

    public override void Init()
    {
        var mateParts = modularCharacters.transform.Find("Modular_Characters")?.Find("Male_Parts");

        torso = new PresetContainer(mateParts?.Find("Male_03_Torso"), "Torso");
        apperArmLeft = new PresetContainer(mateParts?.Find("Male_05_Arm_Upper_Left"), "Apper arm left");
        apperArmRight = new PresetContainer(mateParts?.Find("Male_04_Arm_Upper_Right"), "Apper arm right");
        lowerArmLeft = new PresetContainer(mateParts?.Find("Male_07_Arm_Lower_Left"), "Lower arm left");
        lowerArmRight = new PresetContainer(mateParts?.Find("Male_06_Arm_Lower_Right"), "Lower arm right");
        handLeft = new PresetContainer(mateParts?.Find("Male_09_Hand_Left"), "Hand left");
        handRight = new PresetContainer(mateParts?.Find("Male_08_Hand_Right"), "Hand right");

        SetPrefab(torso);
        SetPrefab(apperArmLeft);
        SetPrefab(apperArmRight);
        SetPrefab(lowerArmLeft);
        SetPrefab(lowerArmRight);
        SetPrefab(handLeft);
        SetPrefab(handRight);

        var buttonTorso = new ButtonPopupList(torso.Name, new List<string>(torso.GetNames()), torso.GetIndex, (index) => SelectionChange(index, torso));
        var buttonApperArmLeft = new ButtonPopupList(apperArmLeft.Name, new List<string>(apperArmLeft.GetNames()), apperArmLeft.GetIndex, (index) => SelectionChange(index, apperArmLeft));
        var buttonApperArmRight = new ButtonPopupList(apperArmRight.Name, new List<string>(apperArmRight.GetNames()), apperArmRight.GetIndex, (index) => SelectionChange(index, apperArmRight));
        var buttonLowerArmLeft = new ButtonPopupList(lowerArmLeft.Name, new List<string>(lowerArmLeft.GetNames()), lowerArmLeft.GetIndex, (index) => SelectionChange(index, lowerArmLeft));
        var buttonLowerArmRight = new ButtonPopupList(lowerArmRight.Name, new List<string>(lowerArmRight.GetNames()), lowerArmRight.GetIndex, (index) => SelectionChange(index, lowerArmRight));
        var buttonHandLeft = new ButtonPopupList(handLeft.Name, new List<string>(handLeft.GetNames()), handLeft.GetIndex, (index) => SelectionChange(index, handLeft));
        var buttonHandRight = new ButtonPopupList(handRight.Name, new List<string>(handRight.GetNames()), handRight.GetIndex, (index) => SelectionChange(index, handRight));

        bodyVisualElement.Add(buttonTorso);
        bodyVisualElement.Add(buttonApperArmLeft);
        bodyVisualElement.Add(buttonApperArmRight);
        bodyVisualElement.Add(buttonLowerArmLeft);
        bodyVisualElement.Add(buttonLowerArmRight);
        bodyVisualElement.Add(buttonHandLeft);
        bodyVisualElement.Add(buttonHandRight);
    }

    public override void Destroy()
    {
        DestroyImmediate(torso.GetPreset());
        DestroyImmediate(apperArmLeft.GetPreset());
        DestroyImmediate(apperArmRight.GetPreset());
        DestroyImmediate(lowerArmLeft.GetPreset());
        DestroyImmediate(lowerArmRight.GetPreset());
        DestroyImmediate(handLeft.GetPreset());
        DestroyImmediate(handRight.GetPreset());
    }
}