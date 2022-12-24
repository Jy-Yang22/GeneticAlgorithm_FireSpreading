using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    //public float burnDistance;
    public ParticleSystem smoke;
    public ParticleSystem fire;
    public ParticleSystem dissolve;

    public Material materialBlack;

    IgniterPlacer igniterPlacer;
    RainManager r1;

    public bool smokeOnStart = false;
    public bool dissolvable = true;

    public float smokeDuration = 3;
    public float fireDuration = 10;

    public bool smoking { get; private set; } = false;
    public bool burning { get; private set; } = false;

    private Vector3 oldPosition;

    private float smokeStart;
    private float fireStart;

    // Start is called before the first frame update
    void Start()
    {
        //StartSmoke();
        oldPosition = transform.position;

        if (smokeOnStart) StartSmoke();

        r1 = FindObjectOfType<RainManager>();
        igniterPlacer = FindObjectOfType<IgniterPlacer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != oldPosition)
        {
            float d = Vector3.Magnitude(transform.position - oldPosition);
            FireManager.Instance.OnBurnableMoved(this.gameObject, d);

            oldPosition = transform.position;
        }
    }

    public void StartSmoke()
    {
        if (this.burning) return;

        smokeStart = Time.time;
        this.StartCoroutine(Smoke(smokeStart));
    }

    public void StartFire()
    {
        fireStart = Time.time;
        this.StartCoroutine(Fire(fireStart));
    }

    public void StopBurning()
    {
        if (this.smoking && !smokeOnStart)
        {
            this.smoking = false;
            this.smoke.Stop();
        }

        if (this.burning)
        {
            this.burning = false;
            FireManager.Instance.OnBurnStopped(this.gameObject);

            this.fire.Stop();
        }
    }

    IEnumerator Smoke(float t)
    {
        this.smoking = true;

        this.smoke.Play();

        yield return new WaitForSeconds(smokeDuration);

        if (this.smoking && t == smokeStart)
        {
            fireStart = Time.time;
            StartCoroutine(Fire(fireStart));
        }
    }

    IEnumerator Fire(float t)
    {
        //Debug.Log("Starting fire");
        this.burning = true;
        this.fire.Play();

        FireManager.Instance.OnBurnStarted(this.gameObject);

        yield return new WaitForSeconds(0.8f);

        if (this.smoking)
        {
            this.smoking = false;
            this.smoke.Stop();
        }

        if (r1.rain.isPlaying == true)
        {
            this.burning = false;
            this.fire.Stop();
            this.smoking = false;
            this.smoke.Stop();
            StopBurning();
        }

        yield return new WaitForSeconds(fireDuration);

        if (this.burning && t == fireStart && dissolvable)
        {
            StartCoroutine(Dissolve());
        }
        else if (r1.rain.isPlaying == true)
        {
            this.burning = false;
            this.fire.Stop();
            this.smoking = false;
            this.smoke.Stop();
            StopBurning();
        }
        else
        {
            this.burning = false;
            this.smoking = false;
            this.smoke.Stop();
            this.fire.Stop();

            this.GetComponent<MeshRenderer>().material = materialBlack;
        }
    }

    IEnumerator Dissolve()
    {
        if (!dissolvable) yield break;

        // If it is no longer burning, then do not dissolve
        // after previous bigfixes, should not be needed anymore
        if (!this.burning)
        {
            yield break;
        }

        if (r1.rain.isPlaying == true)
        {
            this.burning = false;
            this.fire.Stop();
            this.smoking = false;
            this.smoke.Stop();
            StopBurning();
        }

        this.burning = false;
        FireManager.Instance.OnBurnStopped(this.gameObject);

        this.fire.Stop();

        dissolve.Play();

        MeshRenderer mr = GetComponent<MeshRenderer>();
        SkinnedMeshRenderer smr = GetComponentInChildren<SkinnedMeshRenderer>();

        if (mr)
        {
            mr.enabled = false;
        }

        if (smr)
        {
            smr.enabled = false;
        }

        foreach (Collider c in GetComponents<Collider>())
        {
            c.enabled = false;
        }

        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = false;
        }

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
