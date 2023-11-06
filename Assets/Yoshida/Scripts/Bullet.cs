using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private static GameObject hitPrefab;
    private GameObject effect;

    public int launcherLayer;

    private void Awake()
    {
        if (hitPrefab == null)
        {
            hitPrefab = Resources.Load<GameObject>("Hit");
        }
    }

    private void Start()
    {
        Invoke("DestroyAll", 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        int layer = collision.gameObject.layer;
        if (launcherLayer == layer)
        {
            return;
        }
        if (layer == LayerMask.NameToLayer("Player"))
        {
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
        }

        effect = GameObject.Instantiate(hitPrefab);
        effect.transform.position = collision.gameObject.transform.position;
        var renderer = gameObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }
    }

    private void DestroyAll()
    {
        GameObject.Destroy(effect);
        GameObject.Destroy(gameObject);
    }
}
