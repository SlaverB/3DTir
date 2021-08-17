using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private Transform _targetsParent;
    [SerializeField] private int _numOfTargets = 10;
    [SerializeField] private float _sphereRadius = 5.0f;

    private Transform _firstTargetTransform;


    private void Start()
    {
        //InitTargets(transform.position);

        float scaling = 5f;
        Vector3[] pts = PointsOnSphere(12, scaling);
        List<GameObject> targets = new List<GameObject>();
        int i = 0;

        foreach (Vector3 value in pts)
        {
            GameObject target = Instantiate(_target, transform.position, Quaternion.identity, _targetsParent);

            targets.Add(target);
            targets[i].transform.parent = _targetsParent;
            targets[i].transform.position = transform.position + value * scaling;
            targets[i].transform.LookAt(transform.position);
            targets[i].transform.rotation = target.transform.rotation * Quaternion.Euler(90, 0, 0);
            i++;
        }
    }

    private void InitTargets(Vector3 center)
    {
        for (int i = 0; i < _numOfTargets - 1; i++)
        {
            Vector3 pos = RandomSpherePoint(center, _sphereRadius);
            GameObject target = Instantiate(_target, pos, Quaternion.identity, _targetsParent);
            target.transform.LookAt(center);
            target.transform.rotation = target.transform.rotation * Quaternion.Euler(90, 0, 0);
        }
    }

    private Vector3 RandomSpherePoint(Vector3 center, float radius)
    {
        var pos = Random.onUnitSphere;
        pos.y = Mathf.Abs(pos.y);

        pos = center + radius * pos;
        return pos;
    }

    private Vector3[] PointsOnSphere(int n, float scaling)
    {
        List<Vector3> upts = new List<Vector3>();
        float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        float off = 1.0f / n;
        float x = 0;
        float y = 0;
        float z = 0;
        float r = 0;
        float phi = 0;

        for (var k = 0; k < n; k++)
        {
            y = k * off - 1 + (off / 2);
            y = Mathf.Abs(y);
            r = Mathf.Sqrt(1 - y * y);
            phi = k * inc;
            x = Mathf.Cos(phi) * r;
            z = Mathf.Sin(phi) * r;

            Vector3 b = new Vector3(x, y, z);
            Vector3 a = new Vector3(0, 0, 0);
            Vector3 correction = (b - a).normalized * scaling / 2;

            upts.Add(correction);
        }
        Vector3[] pts = upts.ToArray();
        return pts;
    }
}
