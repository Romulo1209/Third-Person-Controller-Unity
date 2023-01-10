using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using PlagueTrain.CharacterStats;

public class StatDisplay : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
{
    private CharacterStat _stat;
    public CharacterStat Stat {
        get { return _stat; }
        set {
            _stat = value;
            UpdateStatValue();
        }
    }

    private string _name;
    public string Name {
        get { return _name; }
        set {
            _name = value;
            nameText.text = _name;
        }
    }

    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text valueText;
    [SerializeField] StatTooltip tooltip;

    private void OnValidate() 
    {
        if (tooltip == null)
            tooltip = FindObjectOfType<StatTooltip>();

        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        nameText = texts[0];
        valueText = texts[1];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowTooltip(Stat, Name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }

    public void UpdateStatValue()
    {
        valueText.text = _stat.Value.ToString();
    }
}
