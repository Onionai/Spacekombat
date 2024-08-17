using UnityEngine;
using UnityEngine.UI;

namespace Onion_AI
{
    public class UIBar : MonoBehaviour
    {
        private Slider barSlider;

        public virtual void Awake()
        {
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
