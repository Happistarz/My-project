using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CardStatController : MonoBehaviour
{
    public enum CardStatType
    {
        CRITICAL,
        ATTACK_SPEED,
        MOVEMENT_SPEED,
        BEACON,
        LENGTH
    }

    public CardStatType cardStatType;
    public float        value;

    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Image    iconImage;
    [SerializeField] private TMP_Text valueText;

    public void Init(CardStatType _type, float _value)
    {
        var image = GetComponent<Image>();

        cardStatType = _type;
        SetStat(_value);

        switch (cardStatType)
        {
            case CardStatType.CRITICAL:

                image.color      = Constants.CRITICAL_COLOR;
                titleText.text   = "Critical";
                iconImage.sprite = Resources.Load<Sprite>("critical");
                break;
            case CardStatType.ATTACK_SPEED:

                image.color      = Constants.ATTACKSPEED_COLOR;
                titleText.text   = "Attack Speed";
                iconImage.sprite = Resources.Load<Sprite>("attack_speed");
                break;
            case CardStatType.MOVEMENT_SPEED:

                image.color      = Constants.MOVEMENTSPEED_COLOR;
                titleText.text   = "Movement Speed";
                iconImage.sprite = Resources.Load<Sprite>("move_speed");
                break;
            case CardStatType.BEACON:

                image.color      = Constants.BEACON_COLOR;
                titleText.text   = "Beacon";
                iconImage.sprite = Resources.Load<Sprite>("beacon");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetStat(float _value)
    {
        valueText.text = $"+{_value}%";
        value = _value;
    }

    public void OnPointerClick()
    {
        GameManager.Instance.ApplyCard(cardStatType, value);
    }
}