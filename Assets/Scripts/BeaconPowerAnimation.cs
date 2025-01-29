using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BeaconPowerAnimation : MonoBehaviour
{
    private static readonly int _START = Animator.StringToHash("Start");

    [SerializeField] private Animator       animator;
    [SerializeField] private AudioSource    audioSource;
    [SerializeField] private ParticleSystem particleSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        transform.localScale = Vector3.one;
    }

    public void StartAnimation()
    {
        gameObject.SetActive(true);
        animator.SetTrigger(_START);
        audioSource.Play();
        particleSystem.Play();
        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(0.1f); // Wait for the animation to start
        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("power") &&
                                         !animator.IsInTransition(0));
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.CompareTag(Constants.ENEMY_TAG))
        {
            _other.gameObject.GetComponent<HealthManager>().TakeDamage(15);
        }
    }
}