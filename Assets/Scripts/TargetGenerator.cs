using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour
{
    public static int FirstTargetIndex;
    public List<Target> Targets = new List<Target>();

    [SerializeField] private Target _target;
    [SerializeField] private Transform _targetsParent;
    [SerializeField] private int _numOfTargets = 10;
    [SerializeField] private float _sphereRadius = 5.0f;

    private void Start()
    {
        float scaling = 3f;
        Vector3[] pts = PointsOnSphere(12, scaling);
        
        int i = 0;

        foreach (Vector3 value in pts)
        {
            Target target = Instantiate(_target, transform.position, Quaternion.identity, _targetsParent) as Target;

            Targets.Add(target);
            Targets[i].transform.parent = _targetsParent;
            Targets[i].transform.position = transform.position + value * scaling;
            Targets[i].transform.LookAt(transform.position);
            Targets[i].transform.rotation = target.transform.rotation * Quaternion.Euler(90, 0, 0);
            i++;
        }

        FirstTargetIndex = Random.Range(0, Targets.Count - 1);
        Targets[FirstTargetIndex].light.SetActive(true);
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
