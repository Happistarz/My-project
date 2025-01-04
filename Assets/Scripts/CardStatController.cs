using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CardStatController : MonoBehaviour, IPointerClickHandler
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
                iconImage.sprite = Resources.Load<Sprite>(Constants.CRITICAL_ICON);
                break;
            case CardStatType.ATTACK_SPEED:

                image.color      = Constants.ATTACKSPEED_COLOR;
                titleText.text   = "Attack Speed";
                iconImage.sprite = Resources.Load<Sprite>(Constants.ATTACK_SPEED_ICON);
                break;
            case CardStatType.MOVEMENT_SPEED:

                image.color      = Constants.MOVEMENTSPEED_COLOR;
                titleText.text   = "Movement Speed";
                iconImage.sprite = Resources.Load<Sprite>(Constants.MOVEMENT_SPEED_ICON);
                break;
            case CardStatType.BEACON:

                image.color      = Constants.BEACON_COLOR;
                titleText.text   = "Beacon";
                iconImage.sprite = Resources.Load<Sprite>(Constants.BEACON_ICON);
                break;
            
            case CardStatType.LENGTH:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetStat(float _value)
    {
        valueText.text = $"+{_value}%";
        value = _value;
    }

    public void OnPointerClick(PointerEventData _eventData)
    {
        // if the event pointer position is not on the card, return
        if (!RectTransformUtility.RectangleContainsScreenPoint((RectTransform) transform, _eventData.position))
            return;
        
        // if the event pointer button is not left, return
        if (_eventData.button != PointerEventData.InputButton.Left)
            return;
        
        GameManager.Instance.EnemyWaveController.ApplyCard(cardStatType, value);
    }
}