using UnityEngine;
using UnityEngine.UI;

public class StatsView : MonoBehaviour
{
    [SerializeField] private CharacterPresets characterPresets;

    [SerializeField] private Text armor;
    [SerializeField] private Text strength;
    [SerializeField] private Text agility;
    [SerializeField] private Text maxSpeed;
    [SerializeField] private Text convenience;

    private void Start()
    {
        characterPresets.onChangeStats += SetStats;
    }

    private void SetStats(CharacterStats stats)
    {
        armor.text = stats.armor.ToString();
        strength.text = stats.strength.ToString();
        agility.text = stats.agility.ToString();
        maxSpeed.text = stats.maxSpeed.ToString();
        convenience.text = stats.convenience.ToString();
    }
}
