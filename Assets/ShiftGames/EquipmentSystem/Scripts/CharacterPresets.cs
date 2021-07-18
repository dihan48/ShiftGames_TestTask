using System;
using UnityEngine;

public class CharacterPresets : MonoBehaviour
{
    [SerializeField] private Transform _partsParent;
    [SerializeField] private Transform[] _bonesForHelmet;
    [SerializeField] private GameObject[] _heads;
    [SerializeField] private GameObject[] _torsos;
    [SerializeField] private GameObject[] _legs;

    [SerializeField] private CharacterStats stats = new CharacterStats();
    private CharacterStats headImpact = new CharacterStats();
    private CharacterStats torsoImpact = new CharacterStats();
    private CharacterStats legsImpact = new CharacterStats();

    public event Action<CharacterStats> onChangeStats;

    private void Start()
    {
        SetHead(0);
        SetTorso(0);
        SetLegs(0);
    }

    public void SetHead(int index)
    {
        if (index >= 0 && index < _heads.Length)
        {
            PrepSetHeadPreset(_heads[index]);
            SetImpact(ref headImpact, _heads[index]);
        }
    }

    public void SetTorso(int index)
    {
        if (index >= 0 && index < _torsos.Length)
        {
            SetPreset(_torsos[index]);
            SetImpact(ref torsoImpact, _torsos[index]);
        }
    }

    public void SetLegs(int index)
    {
        if (index >= 0 && index < _legs.Length)
        {
            SetPreset(_legs[index]);
            SetImpact(ref legsImpact, _legs[index]);
        }
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
            var presetrender = helmetPreset.GetComponent<SkinnedMeshRenderer>();
            render.sharedMesh = presetrender?.sharedMesh;
            render.material = presetrender?.sharedMaterial;
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
                    var presetRender = presetPart.GetComponent<SkinnedMeshRenderer>();

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
                    var presetRender = presetPart.GetComponent<SkinnedMeshRenderer>();

                    if (render != null && presetRender != null)
                        render.sharedMesh = presetRender.sharedMesh;
                }
            }
        }
    }

    private void SetImpact(ref CharacterStats impact, GameObject gameObject)
    {
        var preset = gameObject.GetComponent<BasePreset>();
        impact = new CharacterStats()
        {
            armor = preset.armor,
            strength = preset.strength,
            agility = preset.agility,
            maxSpeed = preset.maxSpeed,
            convenience = preset.convenience
        };

        onChangeStats?.Invoke(stats + headImpact + torsoImpact + legsImpact);
    }
}

[System.Serializable]
public struct CharacterStats
{
    public float armor;
    public float strength;
    public float agility;
    public float maxSpeed;
    public float convenience;

    public static CharacterStats operator +(CharacterStats a, CharacterStats b)
        => new CharacterStats() { 
            armor = a.armor + b.armor,
            strength = a.strength + b.strength,
            agility = a.agility + b.agility,
            maxSpeed = a.maxSpeed + b.maxSpeed,
            convenience = a.convenience + b.convenience
        };
}