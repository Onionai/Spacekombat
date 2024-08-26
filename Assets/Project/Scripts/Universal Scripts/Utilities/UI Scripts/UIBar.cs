using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Onion_AI
{
    public class UIBar : MonoBehaviour
    {
        private Slider barSlider;
        private TMP_Text healthText;

        [field: Header("Status")]
        [field: SerializeField] public CharacterType characterType {get; private set;}

        public virtual void Awake()
        {
            if(characterType == CharacterType.Player)
            {
                barSlider = GetComponentInChildren<Slider>();
                healthText = GetComponentInChildren<TMP_Text>();
                return;
            }
            barSlider = GetComponent<Slider>();
        }

        public virtual void SetCurrentValue(float value)
        {
            if(characterType == CharacterType.Player)
            {
                healthText.text = $"{Mathf.RoundToInt(value)}%";
            }
            barSlider.value = value;
        }

        public virtual void SetMaxValue(float value)
        {
            if(characterType == CharacterType.Player)
            {
                healthText.text = $"{Mathf.RoundToInt(value)}%";
            }
            barSlider.maxValue = value;
            barSlider.value = value;
        }
    }
}
