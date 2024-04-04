using System.Collections;
using UnityEngine;

public class SandStorm : MonoBehaviour
{
    public Light dirLight;

    private ParticleSystem _ps;
    private bool _isStorm = false;

    private void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        StartCoroutine(Weather());
    }

    private void Update()
    {
        if (_isStorm && dirLight.intensity > 0.8f)
            LightIntensity(-1);
        else if (!_isStorm && dirLight.intensity < 1f)
            LightIntensity(1);

    }

    private void LightIntensity(int coef)
    {
        dirLight.intensity += 0.1f * Time.deltaTime * coef;
    }

    IEnumerator Weather()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10f, 20f));

            if (_isStorm)
                _ps.Stop();
            else
                _ps.Play();

            _isStorm = !_isStorm;
        }

    }

}
