using UnityEngine;
using TMPro;
using System.Text;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] RectTransform tooltipRect;

    [SerializeField] TMP_Text ItemNameText;
    [SerializeField] TMP_Text ItemTypeText;
    [SerializeField] TMP_Text ItemDescriptionText;

    private StringBuilder sb = new StringBuilder();
    public void MoveTooltipToMouse() {
        tooltipRect.anchoredPosition = Input.mousePosition;
    }
    public void ShowTooltip(EquippableItem item)
    {
        ItemNameText.text = item.name;
        ItemTypeText.text = item.EquipmentType.ToString();

        sb.Length = 0;
        AddStat(item.StrengthBonus, "Strength");
        AddStat(item.DefenseBonus, "Defense");
        AddStat(item.AgilityBonus, "Agility");
        AddStat(item.VitalityBonus, "Vitality");

        AddStat(item.StrenghtPercentBonus, "Strength", isPercent: true);
        AddStat(item.DefensePercentBonus, "Defense", isPercent: true);
        AddStat(item.AgilityPercentBonus, "Agility", isPercent: true);
        AddStat(item.VitalityPercentBonus, "Vitality", isPercent:true);

        ItemDescriptionText.text = sb.ToString();

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private void AddStat(float value, string statName, bool isPercent = false)
    {
        if(value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (value > 0)
                sb.Append("+");

            if (isPercent) {
                sb.Append(value * 100);
                sb.Append("% ");
            }
            else {
                sb.Append(value);
                sb.Append(" ");
            }
            sb.Append(statName);
        }
    }
}
