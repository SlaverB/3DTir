using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Action ShootInTarget;

    [SerializeField] private TargetGenerator _targetGenerator;
    [SerializeField] private Text _timerText;
    [SerializeField] private GameObject _fpsController;

    private int _targetIndex;
    private bool _isFirstTarget = true;
    private int _shootedTarget;

    private DrawTargetCurve _drawTargetCurve;

    void Start()
    {
        ShootInTarget += HighlightTarget;
        _drawTargetCurve = _fpsController.GetComponent<DrawTargetCurve>();
    }

    private void HighlightTarget()
    {
        if (_isFirstTarget)
        {
            _targetIndex = TargetGenerator.FirstTargetIndex;
            _isFirstTarget = false;
        }

        _targetGenerator.Targets.RemoveAt(_targetIndex);
        _targetIndex = UnityEngine.Random.Range(0, _targetGenerator.Targets.Count - 1);
        _targetGenerator.Targets[_targetIndex].light.SetActive(true);

        for (int i = 0; i < _targetGenerator.Targets.Count - 1; i++)
        {
            _drawTargetCurve.DrawLinearCurve(_targetGenerator.Targets[i].transform.position, _targetGenerator.Targets[i + 1].transform.position);
        }
    }
}
