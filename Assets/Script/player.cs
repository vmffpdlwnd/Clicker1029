using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnRange = 2f;
    public float forceUp = 5f;
    public float coinLifetime = 10f;
    public AudioClip coinSpawnSound;

    private Vector3 originalScale;
    private bool isScaling = false;
    private Rigidbody coinRigidbody;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        originalScale = transform.localScale;
        if (coinPrefab != null)
        {
            coinRigidbody = coinPrefab.GetComponent<Rigidbody>();
            if (coinRigidbody == null)
            {
                Debug.LogError("Rigidbody component not found on the 'coinPrefab' game object.");
            }
        }
        else
        {
            Debug.LogError("'coinPrefab' is null.");
        }

        // Ŭ�� �̺�Ʈ�� ���� ����� �����մϴ�.
        gameObject.layer = LayerMask.NameToLayer("Clickable");
    }

    void Update()
    {
        if (!isScaling && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpawnCoin());
            StartCoroutine(ScaleDown());
        }
    }

    void OnMouseDown()
    {
        if (!isScaling)
        {
            StartCoroutine(SpawnCoin());
            StartCoroutine(ScaleDown());
        }
    }

    IEnumerator SpawnCoin()
    {
        // Player �տ� ������ ��ġ ����
        Vector3 spawnPoint = transform.position + transform.right * Random.Range(-spawnRange, spawnRange) + transform.up * Random.Range(-spawnRange / 2, spawnRange / 2);

        // z ��ǥ�� -6���� ����
        spawnPoint.z = -6.2f;

        spawnPoint.y = Mathf.Max(spawnPoint.y, 2f); // y ��ǥ�� 2 �̻��� �ǵ��� ����

        // ���� ����
        GameObject spawnedCoin = Instantiate(coinPrefab, spawnPoint, Quaternion.identity);
        Debug.Log("������ �����Ǿ����ϴ�.");

        if (audioSource != null && coinSpawnSound != null)
        {
            audioSource.PlayOneShot(coinSpawnSound);
        }

        if (coinRigidbody != null)
        {
            // ���� ���� ���ϱ�
            coinRigidbody.AddForce(Vector3.up * forceUp, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(coinLifetime);
    }

    IEnumerator ScaleDown()
    {
        isScaling = true;

        // ���
        float elapsedTime = 0f;
        float scaleDuration = 0.1f;
        float scaleAmount = 0.9f;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = originalScale * scaleAmount;

        while (elapsedTime < scaleDuration)
        {
            float t = elapsedTime / scaleDuration;
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ���� ũ��� ����
        transform.localScale = originalScale;
        isScaling = false;
    }
}
