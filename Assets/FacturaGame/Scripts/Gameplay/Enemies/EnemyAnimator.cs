using FacturaGame.Scripts.Gameplay.Services.StoperService;
using UnityEngine;
using Zenject;

namespace FacturaGame.Scripts.Gameplay.Enemies
{
    public class EnemyAnimator : MonoBehaviour, IStopable
    {
        private readonly int RunAnimationKey = Animator.StringToHash("IsRunning");
        private readonly int IdleAnimationKey = Animator.StringToHash("IsIdle");
        
        [SerializeField] private Animator _animator;

        [Inject]
        public void Construct(SystemsStopService systemsStopService)
        {
            systemsStopService.AddStopable(this);
        }
        
        public void AnimateWalk()
        {
            _animator.SetBool(RunAnimationKey, false);
        }

        public void AnimateRun()
        {
            _animator.SetBool(RunAnimationKey, true);
        }
        
        public void OnStop()
        {
            _animator.SetBool(IdleAnimationKey, true);
        }
    }
}