using UnityEngine;

namespace UI.Panels
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] private Animator _animator = null;

        public bool IsActive { get; protected set; }

        protected Animator Animator => _animator;

        public virtual void Show()
        {
            if (IsActive)
                return;
            
            IsActive = true;
            
            gameObject.SetActive(true);
            
            _animator.SetBool("IsActive", IsActive);
        }
        
        public virtual void Hide()
        {
            if (!IsActive)
                return;

            IsActive = false;
            
            _animator.SetBool("IsActive", IsActive);
        }

        private void DisablePanel()
        {
            if (!IsActive)
            {
                gameObject.SetActive(false);
            }
        }
    }
}