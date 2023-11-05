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

        // 클릭 이벤트를 받을 대상을 설정합니다.
        gameObject.layer = LayerMask.NameToLayer("Clickable");
    }

    void Update()
    {
        if (!isScaling && Input.GetKeyDown(KeyCode.Space) | Input.GetMouseButtonDown(0) && gameObject.layer == LayerMask.NameToLayer("Clickable"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                StartCoroutine(SpawnCoin());
                StartCoroutine(ScaleDown());
            }
        }
    }

    IEnumerator SpawnCoin()
    {
        // Player 앞에 랜덤한 위치 설정
        Vector3 spawnPoint = transform.position + transform.right * Random.Range(-spawnRange, spawnRange) + transform.up * Random.Range(-spawnRange / 2, spawnRange / 2);

        spawnPoint.z = -6.2f;

        spawnPoint.y = Mathf.Max(spawnPoint.y, 2f); // y 좌표가 2 이상이 되도록 설정

        // 코인 생성
        GameObject spawnedCoin = Instantiate(coinPrefab, spawnPoint-new Vector3(0.25f,0,0), Quaternion.identity);

        if (audioSource != null && coinSpawnSound != null)
        {
            audioSource.PlayOneShot(coinSpawnSound);
        }

        if (coinRigidbody != null)
        {
            // 힘을 위로 가하기
            coinRigidbody.AddForce(Vector3.up * forceUp, ForceMode.Impulse);
        }

        yield return YieldInstructionCache.WaitForSeconds(coinLifetime);
    }

    IEnumerator ScaleDown()
    {
        isScaling = true;

        // 축소
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

        // 원래 크기로 복원
        transform.localScale = originalScale;
        isScaling = false;
    }
}
