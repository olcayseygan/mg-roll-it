using System;
using System.Collections.Generic;
using Assets.Scripts.Patterns.StatePattern;
using UnityEngine;

namespace Assets.Scripts.States.CubeStates
{
    public enum FaceDirection
    {
        Forward,
        Back,
        Right,
        Left,
        Up,
        Down
    }

    public class MotionState : State<Cube>
    {
        private readonly Dictionary<FaceDirection, Vector3> _faceDirections = new();
        private float _timeElapsed = 0.0f;
        private float _motionDuration = 0.0f;

        private Vector3 _initialEulerAngle = Vector3.zero;
        private Vector3 _targetEulerAngle = Vector3.zero;

        public override StateTransition<Cube> OnEnter(Cube self)
        {
            _faceDirections[FaceDirection.Forward] = self.modelTransform.forward;
            _faceDirections[FaceDirection.Back] = -self.modelTransform.forward;
            _faceDirections[FaceDirection.Right] = self.modelTransform.right;
            _faceDirections[FaceDirection.Left] = -self.modelTransform.right;
            _faceDirections[FaceDirection.Up] = self.modelTransform.up;
            _faceDirections[FaceDirection.Down] = -self.modelTransform.up;

            var groundFace = DetermineMaxDotFace(Vector3.down);
            var targetFace = DetermineMaxDotFace(self.direction);

            _initialEulerAngle = new Vector3(0.0f, 0.0f, 0.0f);
            _targetEulerAngle = new Vector3(self.direction.z * 90f, 0.0f, self.direction.x * -90f);
            UpdateTransformWhileKeepingModel(self, () =>
            {
                var distance = 0.5f;
                if (targetFace == FaceDirection.Up || targetFace == FaceDirection.Down)
                {
                    distance = 1.0f;
                }

                self.transform.SetPositionAndRotation(self.lastKnownPosition + self.direction * distance, Quaternion.identity);
            });

            _motionDuration = Game.I.speed;
            _timeElapsed = _motionDuration;
            return base.OnEnter(self);
        }

        private FaceDirection DetermineMaxDotFace(Vector3 direction)
        {
            var maxDot = -1.0f;
            var maxIndex = FaceDirection.Forward;
            foreach (var faceDirection in _faceDirections)
            {
                var dot = Vector3.Dot(direction, faceDirection.Value);
                if (dot > maxDot)
                {
                    maxDot = dot;
                    maxIndex = faceDirection.Key;
                }
            }

            return maxIndex;
        }

        public override void OnExit(Cube self)
        {
            base.OnExit(self);
            self.lastKnownPosition.x = self.modelTransform.transform.position.x;
            self.lastKnownPosition.y = 0f;
            self.lastKnownPosition.z = self.modelTransform.transform.position.z;
            Game.I.AddCurrentRunScore(1);
            GameUI.I.playingPanel.SetScoreText(Game.I.GetCurrentRunScore());

        }

        public override StateTransition<Cube> Update(Cube self)
        {
            _timeElapsed = Mathf.Max(0.0f, _timeElapsed - Time.deltaTime);
            var t = self.motionCurve.Evaluate(1.0f - _timeElapsed / _motionDuration);
            self.transform.rotation = Quaternion.Euler(new Vector3(
                Mathf.LerpAngle(_initialEulerAngle.x, _targetEulerAngle.x, t),
                Mathf.LerpAngle(_initialEulerAngle.y, _targetEulerAngle.y, t),
                Mathf.LerpAngle(_initialEulerAngle.z, _targetEulerAngle.z, t)
            ));
            if (_timeElapsed <= 0.0f) {
                return self.StateProvider.FindState<IdleState>();
            }

            return base.Update(self);
        }

        private void UpdateTransformWhileKeepingModel(Cube self, Action action)
        {
            self.modelTransform.transform.GetPositionAndRotation(out var position, out var rotation);
            action();
            self.modelTransform.transform.SetPositionAndRotation(position, rotation);
        }
    }
}