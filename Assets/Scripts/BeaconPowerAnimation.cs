using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BeaconPowerAnimation : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;
    
    [SerializeField] private Material material;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        transform.localScale = Vector3.one;
        
        material = GetComponent<MeshRenderer>().material;
    }

    private IEnumerator Animate(float _duration, float _maxScale)
    {
        var timer = 0.0f;
        var startScale = transform.localScale;
        var endScale = Vector3.one * _maxScale;
        var startColor = material.color;
        var endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

        while (timer < _duration)
        {
            timer += Time.deltaTime;
            var t = animationCurve.Evaluate(timer / _duration);
            var scale = Mathf.Lerp(1.0f, _maxScale, t);
            transform.localScale = Vector3.one * scale;
            material.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        transform.localScale = endScale;
        material.color = endColor;
        gameObject.SetActive(false);
        transform.localScale = startScale;
        material.color = startColor;
    }

    public void StartAnimation()
    {
        gameObject.SetActive(true);
        StartCoroutine(Animate(3.5f, 7.0f));
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.CompareTag(Constants.ENEMY_TAG))
        {
            _other.gameObject.GetComponent<HealthManager>().TakeDamage(15);
        }
    }
}
