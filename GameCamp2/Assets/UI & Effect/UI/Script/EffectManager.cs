using System.Collections;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject effect;

    public void EffectInstance()
    {
        StartCoroutine("EffectDelay");
    }

    IEnumerator EffectDelay()
    {
        GameObject effectClone = Instantiate(effect.gameObject, transform.position, Quaternion.identity) as GameObject;
        effectClone.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1);
        Destroy(effectClone);
    }
}
