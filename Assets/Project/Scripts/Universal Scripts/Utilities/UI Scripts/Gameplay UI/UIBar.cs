using UnityEngine;
using UnityEngine.UI;

namespace Onion_AI
{
    public class UIBar : MonoBehaviour
    {
        private Slider barSlider;

        [field: Header("Status")]
        [field: SerializeField] public CharacterType characterType {get; private set;}

        public virtual void Awake()
        {
            if(characterType == CharacterType.Player)
            {
                barSlider = GetComponentInChildren<Slider>();
                return;
            }
            barSlider = GetComponent<Slider>();
        }

        public virtual void SetCurrentValue(float value)
        {
            barSlider.value = value;
        }

        public virtual void SetMaxValue(float value)
        {
            barSlider.maxValue = value;
            barSlider.value = value;
        }
    }
}
