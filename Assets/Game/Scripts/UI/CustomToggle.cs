using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Animator))]
    public class CustomToggle : MonoBehaviour
    {
        public event Action<bool> OnChangedValue = null;
    
        private Animator _animator = null;
        private Button _button = null;

        private bool _isOn = false;
        
        public bool IsOn 
        { 
            get => _isOn;
            set
            {
                _isOn = value;
                
                OnChangedValue?.Invoke(_isOn);
                
                UpdateState();
            } 
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _button = GetComponent<Button>();

            _button.onClick.AddListener(() =>
            {
                IsOn = !IsOn;
            });
            
            SetState();
        }

        private void OnEnable()
        {
            SetState();
        }

        private void SetState()
        {
            _animator.Play(IsOn ? "On" : "Off");
            _animator.SetBool("IsOn", IsOn);
        }

        private void UpdateState()
        {
            _animator.SetBool("IsOn", _isOn);
        }
    }
}