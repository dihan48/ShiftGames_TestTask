using UnityEngine;

public class PresetContainer
{
    public bool IsValid { get; private set; } = true;
    public string Name { get; private set; }
    private readonly Transform variantsParent;
    private GameObject preset;
    private string[] names;
    private int selectedIndex;

    public PresetContainer(Transform variantsParent, string name)
    {
        this.variantsParent = variantsParent;
        Name = name;
        Init();
    }

    public GameObject GetPreset() => preset;

    public GameObject SetPreset(GameObject preset)
    {
        if (IsValid)
            this.preset = preset;

        return preset;
    }

    public int GetIndex() => selectedIndex;

    public string[] GetNames() => (string[])names.Clone();

    public void SetIndex(int index)
    {
        if (IsValid)
            selectedIndex = index;
    }

    public GameObject GetVariant()
    {
        if (IsValid == false)
            return null;

        return variantsParent.GetChild(selectedIndex).gameObject;
    }

    private void Init()
    {
        if (variantsParent != null)
        {
            names = new string[variantsParent.childCount];

            for (int i = 0; i < variantsParent.childCount; i++)
            {
                names[i] = variantsParent.GetChild(i).name;
            }
        }
        else
        {
            IsValid = false;
        }
    }
}