using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform partsParent;
    [SerializeField] private Transform upperBoneHelmet;
    [SerializeField] private Transform lowerBoneHelmet;
    [SerializeField] private Transform weaponAttachment;
    [SerializeField] private Vector3 weaponPositionOffset = new Vector3(0,0,0);
    [SerializeField] private Quaternion weaponRotationOffset = new Quaternion(-0.133961096f, -0.627931356f, 0.760417044f, -0.0975840911f);

    [Header("Presets")]
    [SerializeField] private GameObject[] heads;
    [SerializeField] private GameObject[] torsos;
    [SerializeField] private GameObject[] legs;
    [SerializeField] private GameObject[] weapons;

    [SerializeField] private CharacterStats stats = new CharacterStats();

    private GameObject weapon;

    private CharacterStats headImpact = new CharacterStats();
    private CharacterStats torsoImpact = new CharacterStats();
    private CharacterStats legsImpact = new CharacterStats();
    private CharacterStats weaponImpact = new CharacterStats();

    public event Action<CharacterStats> onChangeStats;

    private void Start()
    {
        SetHead(0);
        SetTorso(0);
        SetLegs(0);
    }

    public void SetHead(int index)
    {
        if (index >= 0 && index < heads.Length)
        {
            PrepSetHeadPreset(heads[index]);
            SetImpact(ref headImpact, heads[index]);
        }
    }

    public void SetTorso(int index)
    {
        if (index >= 0 && index < torsos.Length)
        {
            SetPreset(torsos[index]);
            SetImpact(ref torsoImpact, torsos[index]);
        }
    }

    public void SetLegs(int index)
    {
        if (index >= 0 && index < legs.Length)
        {
            SetPreset(legs[index]);
            SetImpact(ref legsImpact, legs[index]);
        }
    }

    public void SetWeapon(int index)
    {
        if (weapon != null)
            Destroy(weapon);

        if (index > 0 && index < legs.Length)
        {
            weapon = Instantiate(weapons[index - 1], weaponAttachment);
            weapon.transform.localPosition = weaponPositionOffset;
            weapon.transform.rotation = weaponRotationOffset;
            weapon.transform.localScale = Vector3.one * 100;

            SetImpact(ref weaponImpact, weapons[index - 1]);
        }
        else if(index == 0)
        {
            SetImpact(ref weaponImpact, null);
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

        for (int i = 0; i < partsParent.childCount; i++)
        {
            var part = partsParent.GetChild(i).gameObject;

            if (part.name == "Helmet")
                Destroy(part);

            if (Array.IndexOf(conflictingObjectNames, part.name) != -1)
                part.SetActive(!hasHelmet);
        }

        if (hasHelmet)
        {
            var helmet = new GameObject { name = "Helmet" };
            helmet.transform.parent = partsParent;

            var render = helmet.AddComponent<SkinnedMeshRenderer>();
            var presetrender = helmetPreset.GetComponent<SkinnedMeshRenderer>();
            render.sharedMesh = presetrender?.sharedMesh;
            render.material = presetrender?.sharedMaterial;
            render.bones = new[] { upperBoneHelmet, lowerBoneHelmet };
            render.rootBone = lowerBoneHelmet;
        }
    }

    private void SetHeadPreset(GameObject[] presetParts)
    {
        for (int i = 0; i < presetParts.Length; i++)
        {
            for (int j = 0; j < partsParent.childCount; j++)
            {
                var part = partsParent.GetChild(j).gameObject;
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
            for (int j = 0; j < partsParent.childCount; j++)
            {
                var presetPart = preset.transform.GetChild(i).gameObject;
                var part = partsParent.GetChild(j).gameObject;

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
        if(gameObject != null)
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
        }
        else
        {
            impact = new CharacterStats();
        }

        onChangeStats?.Invoke(stats + headImpact + torsoImpact + legsImpact + weaponImpact);
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