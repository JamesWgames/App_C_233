using UnityEngine;
using DG.Tweening;
using System.Globalization;
using UnityEngine.UI;
using Slots.Game.Values;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class MoneyDisplay : MonoBehaviour
    {
        private Text _text = null;

        private Tween _counterTween = null;
        
        private void Awake()
        {
            var format = new NumberFormatInfo { NumberGroupSeparator = " " };

            _text = GetComponent<Text>();
            _text.text = Wallet.Money.ToString("#,0", format);

            Wallet.OnChangedMoney += UpdateMoney;
        }
        
        private void OnDestroy()
        {
            Wallet.OnChangedMoney -= UpdateMoney;
        }

        private void UpdateMoney(int count)
        {
            if (_counterTween != null && _counterTween.IsActive())
                _counterTween.Kill();

            var format = new NumberFormatInfo { NumberGroupSeparator = " " };

            _counterTween = DOVirtual.Int(int.Parse(_text.text.Replace(" ", "")), count, 0.35f,
                (value) => _text.text = value.ToString("#,0", format)).Play();
        }
    }
}