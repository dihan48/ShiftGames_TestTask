using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;

[CustomEditor(typeof(HeadPreset))]
public class HeadPresetEditor : BasePresetEditor
{
    private PresetContainer head;
    private PresetContainer helmet;
    private ButtonPopupList buttonHead;
    private ButtonPopupList buttonHelmet;

    public override void OnEnable()
    {
        base.OnEnable();

        if (StageUtility.GetCurrentStage() == StageUtility.GetMainStage())
        {
            var button = new Button(() => CustomMenuPresetEditor.Head()) { text = "Open Peset Editor" };
            rootVisualElement.Add(button);
            return;
        }

        var mateHeadParts = modularCharacters.transform.Find("Modular_Characters")?.Find("Male_Parts")?.Find("Male_00_Head");
        head = new PresetContainer(mateHeadParts?.Find("Male_Head_All_Elements"), "head");
        helmet = new PresetContainer(mateHeadParts?.Find("Male_Head_No_Elements"), "helmet");
        SetPrefab(head);

        buttonHead = new ButtonPopupList(head.Name, new List<string>(head.GetNames()), head.GetIndex, (index) => SelectionChange(index, head));
        buttonHelmet = new ButtonPopupList(helmet.Name, new List<string>(helmet.GetNames()), helmet.GetIndex, (index) => SelectionChange(index, helmet));
        buttonHelmet.SetEnabled(false);

        var hasHelmet = new Toggle("Has Helmet");
        hasHelmet.RegisterValueChangedCallback(HasHelmetChanged);

        rootVisualElement.Add(hasHelmet);

        rootVisualElement.Add(buttonHead);
        rootVisualElement.Add(buttonHelmet);
    }

    private void HasHelmetChanged(ChangeEvent<bool> evt)
    {
        if (evt.newValue)
        {
            DestroyImmediate(head.GetPreset());
            SetPrefab(helmet);
            buttonHead.SetEnabled(false);
            buttonHelmet.SetEnabled(true);
        }
        else
        {
            DestroyImmediate(helmet.GetPreset());
            SetPrefab(head);
            buttonHead.SetEnabled(true);
            buttonHelmet.SetEnabled(false);
        }
    }

    public void OnDisable()
    {
        if(head.GetPreset() != null)
            DestroyImmediate(head.GetPreset());
        if(helmet.GetPreset() != null)
            DestroyImmediate(helmet.GetPreset());
    }
}