using System;
using UnityEngine;

public class CharacterPresets : MonoBehaviour
{
    [SerializeField] private Transform _partsParent;
    [SerializeField] private Transform[] _bonesForHelmet;
    [SerializeField] private GameObject[] _heads;
    [SerializeField] private GameObject[] _torsos;
    [SerializeField] private GameObject[] _legs;

    public void SetHead(int index)
    {
        if (index >= 0 && index < _heads.Length)
            PrepSetHeadPreset(_heads[index]);
    }

    public void SetTorso(int index)
    {
        if (index >= 0 && index < _torsos.Length)
            SetPreset(_torsos[index]);
    }

    public void SetLegs(int index)
    {
        if (index >= 0 && index < _legs.Length)
            SetPreset(_legs[index]);
    }

    private void PrepSetHeadPreset(GameObject preset)
    {
        GameObject helmet = null;
        var presetParts = new GameObject[preset.transform.childCount];

        for (int i = 0; i < preset.transform.childCount; i++)
        {
            var presetpart = preset.transform.GetChild(i).gameObject;

            if (presetpart.name == "Helmet")
                helmet = presetpart;

            presetParts[i] = presetpart;
        }

        SetHelmetPreset(helmet);
        SetHeadPreset(presetParts);
    }

    private void SetHelmetPreset(GameObject helmetPreset)
    {
        var conflictingObjectNames = new string[] { "Head", "Face hair", "Eyebrow" };
        var hasHelmet = helmetPreset != null;

        for (int i = 0; i < _partsParent.childCount; i++)
        {
            var part = _partsParent.GetChild(i).gameObject;

            if (part.name == "Helmet")
                Destroy(part);

            if (Array.IndexOf(conflictingObjectNames, part.name) != -1)
                part.SetActive(!hasHelmet);
        }

        if (hasHelmet)
        {
            var helmet = new GameObject { name = "Helmet" };
            helmet.transform.parent = _partsParent;

            var render = helmet.AddComponent<SkinnedMeshRenderer>();
            render.sharedMesh = helmetPreset.GetComponent<MeshFilter>()?.sharedMesh;
            render.material = helmetPreset.GetComponent<MeshRenderer>()?.sharedMaterial;
            /*
                * I didn't find any normal documentation on specifying bones for SkinnedMeshRenderer,
                * but analyzing the following code from the documentation 
                * https://docs.unity3d.com/ScriptReference/Mesh-boneWeights.html
                * helped me understand that need to specify array Transform bones
                * to the bones property in SkinnedMeshRenderer
                * otherwise the mesh will not be displayed
                * I spent about 3 days searching for and understanding this feature of fucking unity
            */
            render.bones = _bonesForHelmet;
            render.rootBone = _bonesForHelmet[_bonesForHelmet.Length - 1];
        }
    }

    private void SetHeadPreset(GameObject[] presetParts)
    {
        for (int i = 0; i < presetParts.Length; i++)
        {
            for (int j = 0; j < _partsParent.childCount; j++)
            {
                var part = _partsParent.GetChild(j).gameObject;
                var presetPart = presetParts[i];

                if (presetPart.name == part.name)
                {
                    if (part.activeSelf == false)
                        part.SetActive(true);

                    var render = part.GetComponent<SkinnedMeshRenderer>();
                    var presetRender = presetPart.GetComponent<MeshFilter>();

                    if (render != null && presetRender != null)
                        render.sharedMesh = presetRender.sharedMesh;
                }
            }
        }
    }

    private void SetPreset(GameObject preset)
    {
        for (int i = 0; i < preset.transform.childCount; i++)
        {
            for (int j = 0; j < _partsParent.childCount; j++)
            {
                var presetPart = preset.transform.GetChild(i).gameObject;
                var part = _partsParent.GetChild(j).gameObject;

                if (part != null && presetPart != null && part.name == presetPart.name)
                {
                    var render = part.GetComponent<SkinnedMeshRenderer>();
                    var presetRender = presetPart.GetComponent<MeshFilter>();

                    if (render != null && presetRender != null)
                        render.sharedMesh = presetRender.sharedMesh;
                }
            }
        }
    }
}
