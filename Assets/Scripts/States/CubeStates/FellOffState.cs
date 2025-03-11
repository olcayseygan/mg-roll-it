using System.Collections;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.CubeStates
{
    public class FellOffStateProperties : StateProperties
    {
        public FaceDirection targetFace;
    }

    public class FellOffState : State<Cube, FellOffStateProperties>
    {
        private const float DURATION = 1.5f;
        private float _timer = DURATION;

        private GameObject _deathEffectGameObject;

        public override StateTransition<Cube> OnEnter(Cube self, FellOffStateProperties properties)
        {
            AudioManager.I.PlaySFX(AudioManager.I.cubeDeathClip);
            _timer = DURATION;
            _deathEffectGameObject = Object.Instantiate(Game.I.deathEffectPrefab, self.modelTransform.position, self.modelTransform.rotation);
            ChangeAllChildrenActivity(self, false);
            return base.OnEnter(self);
        }

        public override void OnExit(Cube self)
        {
            base.OnExit(self);
        }

        public override StateTransition<Cube> Update(Cube self)
        {

            _timer = Mathf.Max(0f, _timer - Time.deltaTime);
            if (_timer <= 0f && !Game.I.StateProvider.IsInState<GameStates.GameOverState>())
            {
                Game.I.StateProvider.SwitchTo<GameStates.GameOverState>();
                Object.Destroy(_deathEffectGameObject);
                _timer = DURATION;
                ChangeAllChildrenActivity(self, true);
            }

            return base.Update(self);
        }

        private void ChangeAllChildrenActivity(Cube self, bool active)
        {
            foreach (Transform child in self.transform)
            {
                if (child.TryGetComponent<ParticleSystem>(out var particleSystem))
                {
                    if (active)
                    {
                        particleSystem.Play();
                    }
                    else
                    {
                        particleSystem.Stop();
                    }
                }
                else
                {
                    child.gameObject.SetActive(active);
                }
            }
        }
    }
}