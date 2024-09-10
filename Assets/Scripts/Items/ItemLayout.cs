using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Items
{
    public class ItemLayout : MonoBehaviour
    {
        public Image uIcon;
        public TextMeshProUGUI uiValue;

        private ItemSetup _currentSetup;

        public void Load(ItemSetup setup)
        {
            _currentSetup = setup;
            UpdateUI();
        }

        private void UpdateUI()
        {
            uIcon.sprite = _currentSetup.icon;
        }

        private void Update()
        {
            uiValue.text = _currentSetup.soInt.value.ToString();
        }
    }
}
