using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedProjectile : Projectille
{
    [SerializeField]float _StartVelocity;
    [SerializeField]float _Angle;

    [SerializeField] Transform _FirePoint;
    public float projectileDestroyTimer = 1f;
    // [SerializeField] float _Height;
    private void Start()
    {
        Destroy(gameObject, projectileDestroyTimer);
       
        //_FirePoint.position = transform.position;
    }
    public override void MoveProjectile()
    {
       // base.MoveProjectile();
    }

    private void Update()
    {
        Vector3 direction = target - projectileSpawnPoint.position;
        Vector3 groundDirection = new Vector3(direction.x, 0, direction.z);
        Vector3 targetPos = new Vector3(groundDirection.magnitude, direction.y, 0);

        float height = targetPos.y + targetPos.magnitude / 2f;
        height = Mathf.Max(0.01f, height);
        float angle;
        float v0;
        float time;
        CalculatePathWithHeight(targetPos, height,out v0,out angle,out time);
       // StopAllCoroutines();
        StartCoroutine(Corutine_Movement(groundDirection.normalized,v0, angle,time));
    }

    float QuadraticEquation(float a,float b,float c,float sign)
    {
        return (-b + sign * Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
    }
    void CalculatePathWithHeight(Vector3 targetPos,float h,out float v0,out float angle,out float time)
    {
        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float b = Mathf.Sqrt(2 * g * h);
        float a = (-0.5f * g);
        float c = -yt;

        float tplus = QuadraticEquation(a, b, c, 1);
        float tmin = QuadraticEquation(a, b, c, -1);
        time = tplus > tmin ? tplus : tmin;

        angle = Mathf.Atan(b * time / xt);

        v0 = b / Mathf.Sin(angle);

    } //www.youtube.com/watch?v=Qxs3GrhcZI8&t=178s&ab_channel=ABitOfGameDev
    void CalculatePath(Vector3 targetPos,float angle,out float v0, out float time)
    {
        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float v1 = Mathf.Pow(xt, 2) * g;
        float v2 = 2 * xt * Mathf.Sin(angle) * Mathf.Cos(angle);
        float v3 = 2 * yt * Mathf.Pow(Mathf.Cos(angle), 2);
        v0 = Mathf.Sqrt(v1 / (v2 - v3));

        time = xt / (v0 * Mathf.Cos(angle));
    }

    IEnumerator Corutine_Movement(Vector3 direction,float v0,float angle,float time)
    {
        float t = 0;
        while(t < time)
        {
            float x = v0 * t * Mathf.Cos(angle);
            float y = v0 * t * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2);
            transform.position = projectileSpawnPoint.position + direction * x +Vector3.up * y;

            t += Time.deltaTime;
            yield return null;
        }

    }


}
