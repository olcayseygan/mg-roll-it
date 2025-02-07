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

        private Vector3 _startEulerAngle = Vector3.zero;
        private Vector3 _targetEulerAngle = Vector3.zero;

        private Vector3 _sourcePivotPosition = Vector3.zero;
        private Vector3 _targetPivotPosition = Vector3.zero;

        private Vector3 _sourceModelPosition = Vector3.zero;
        private Vector3 _targetModelPosition = Vector3.zero;


        public override StateTransition<Cube> OnEnter(Cube self)
        {
            _faceDirections[FaceDirection.Forward] = self.animationModelTranform.forward;
            _faceDirections[FaceDirection.Back] = -self.animationModelTranform.forward;
            _faceDirections[FaceDirection.Right] = self.animationModelTranform.right;
            _faceDirections[FaceDirection.Left] = -self.animationModelTranform.right;
            _faceDirections[FaceDirection.Up] = self.animationModelTranform.up;
            _faceDirections[FaceDirection.Down] = -self.animationModelTranform.up;

            var groundFace = DetermineMaxDotFace(Vector3.down);
            var targetFace = DetermineMaxDotFace(self.direction);
            Debug.Log($"Ground face: {groundFace}, Target face: {targetFace}");

            _startEulerAngle = new Vector3(
                groundFace switch
                {
                    FaceDirection.Down => 0.0f,
                    FaceDirection.Forward => 90.0f,
                    FaceDirection.Up => 180.0f,
                    FaceDirection.Back => 270.0f,
                    FaceDirection.Left => 0.0f,
                    FaceDirection.Right => 0.0f,
                    _ => 0.0f
                },
                0.0f,
                0.0f);

            switch (groundFace)
            {
                case FaceDirection.Down:
                    _targetEulerAngle = new Vector3(
                        _startEulerAngle.x + targetFace switch
                        {
                            FaceDirection.Forward => 90f,
                            FaceDirection.Back => -90f,
                            FaceDirection.Left => 90f,
                            FaceDirection.Right => -90f,
                            _ => 0.0f
                        },
                        _startEulerAngle.y,
                        _startEulerAngle.z);
                    break;

                case FaceDirection.Forward:
                    _targetEulerAngle = new Vector3(
                        _startEulerAngle.x + targetFace switch
                        {
                            FaceDirection.Up => 90,
                            FaceDirection.Down => -90,
                            FaceDirection.Left => -90,
                            FaceDirection.Right => 90,
                            _ => 0.0f
                        },
                        _startEulerAngle.y,
                        _startEulerAngle.z);
                    break;

                case FaceDirection.Up:
                    _targetEulerAngle = new Vector3(
                        _startEulerAngle.x + targetFace switch
                        {
                            FaceDirection.Back => 90.0f,
                            FaceDirection.Forward => -90.0f,
                            FaceDirection.Left => -90.0f,
                            FaceDirection.Right => 90.0f,
                            _ => 0.0f
                        },
                        _startEulerAngle.y,
                        _startEulerAngle.z);
                    break;

                case FaceDirection.Back:
                    _targetEulerAngle = new Vector3(
                        _startEulerAngle.x + targetFace switch
                        {
                            FaceDirection.Down => 90f,
                            FaceDirection.Up => -90f,
                            FaceDirection.Left => 90f,
                            FaceDirection.Right => -90f,
                            _ => 0.0f
                        },
                        _startEulerAngle.y,
                        _startEulerAngle.z);
                    break;

                case FaceDirection.Left:
                    _targetEulerAngle = new Vector3(
                        _startEulerAngle.x + targetFace switch
                        {
                            FaceDirection.Forward => -90f,
                            FaceDirection.Back => 90f,
                            FaceDirection.Up => 90f,
                            FaceDirection.Down => -90f,
                            _ => 0.0f
                        },
                        _startEulerAngle.y,
                        _startEulerAngle.z);
                    break;

                case FaceDirection.Right:
                    _targetEulerAngle = new Vector3(
                        _startEulerAngle.x + targetFace switch
                        {
                            FaceDirection.Forward => 90f,
                            FaceDirection.Back => -90f,
                            FaceDirection.Up => 90f,
                            FaceDirection.Down => -90f,
                            _ => 0.0f
                        },
                        _startEulerAngle.y,
                        _startEulerAngle.z);
                    break;
            }

            _sourceModelPosition = new Vector3(
                0.0f,
                (groundFace == FaceDirection.Down || groundFace == FaceDirection.Up) ? 1.0f : -1.0f,
                -0.5f);
            _targetModelPosition = new Vector3(
                0.0f,
                (targetFace == FaceDirection.Down || targetFace == FaceDirection.Up) ? 1.0f : -1.0f,
                -0.5f
            );

            _sourcePivotPosition = self.animationPivotTransform.localPosition;
            _targetPivotPosition = new Vector3(
                _sourcePivotPosition.x,
                _sourcePivotPosition.y,
                _sourcePivotPosition.z + (groundFace == FaceDirection.Down || groundFace == FaceDirection.Up ? 2.0f : 0.0f)
            );

            _timeElapsed = self.motionDuration;
            Debug.Log($"Start Euler: {_startEulerAngle}, Target Euler: {_targetEulerAngle}");
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
            // self.transform.position = self.transform.position + self.direction * (_isLongSideDown ? 2.0f : 1.0f);
            // self.animationModelTranform.localPosition = new Vector3(0.0f, -1f, -0.5f);
            base.OnExit(self);
        }

        public override StateTransition<Cube> Update(Cube self)
        {
            _timeElapsed = Mathf.Max(0.0f, _timeElapsed - Time.deltaTime);

            var t = self.motionCurve.Evaluate(1.0f - _timeElapsed / self.motionDuration);
            self.animationPivotTransform.localRotation = Quaternion.Euler(new Vector3(
                Mathf.LerpAngle(_startEulerAngle.x, _targetEulerAngle.x, t),
                Mathf.LerpAngle(_startEulerAngle.y, _targetEulerAngle.y, t),
                Mathf.LerpAngle(_startEulerAngle.z, _targetEulerAngle.z, t)
            ));
            self.animationPivotTransform.localPosition = Vector3.Lerp(_sourcePivotPosition, _targetPivotPosition, t);
            self.animationModelTranform.localPosition = Vector3.Lerp(_sourceModelPosition, _targetModelPosition, t);
            if (_timeElapsed <= 0.0f)
                return self.StateProvider.FindState<IdleState>();

            return base.Update(self);
        }
    }
}