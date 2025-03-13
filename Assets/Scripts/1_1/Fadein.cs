using UnityEngine;
using System.Collections;
using Unity.VisualScripting.Dependencies.Sqlite;

public class Fadein : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float fadeDuration = 2f;
    private float timeElapsed = 0f;
    private Camera mainCamera;

    public Vector3 targetCameraPosition = new Vector3(3f, -1.5f, -10f); // 카메라 목표 위치
    public float cameraMoveDuration = 2f;  // 카메라 이동 시간
    public float targetCameraSize = 3f;  // 카메라 줌인값

    public GameObject fox; // 여우 오브젝트
    public GameObject player; //플레이어 오브젝트
    public float foxSpeed = 2f; // 여우 이동 속도
    public float playerSpeed = 2f; //플레이어 이동 속도
    public Vector2 foxPosition = new Vector2(10f, 0f); // 여우 이동 방향
    public Vector2 playerPosition = new Vector2(10f, 0f); // 플레이어 이동 방향

    private Rigidbody2D foxRb; // 여우의 Rigidbody2D 컴포넌트
    private Rigidbody2D playerRb; // 플레이어 Rigidbody2D 컴포넌트

    void Start()
    {
        foxRb = fox.GetComponent<Rigidbody2D>();
        playerRb = player.GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;

        Color startColor = spriteRenderer.color;
        startColor.a = 1f;
        spriteRenderer.color = startColor;


        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        // 페이드 인 효과
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Clamp(1 - (timeElapsed / fadeDuration), 0f, 1f);
            Color newColor = spriteRenderer.color;
            newColor.a = alpha;
            spriteRenderer.color = newColor;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        StartCoroutine(MovePlayerCoroutine());

        yield return new WaitForSeconds(5f);

        StartCoroutine(MoveCameraCoroutine());

        yield return new WaitForSeconds(2f);

        StartCoroutine(FoxMoveCoroutine());

    }

    IEnumerator MovePlayerCoroutine()
    {
        while (player.transform.position.x < playerPosition.x)
        {
            Vector2 direction = ((Vector2)transform.position - playerPosition).normalized;
            Debug.Log(direction);
            playerRb.linearVelocity = direction * playerSpeed;
            yield return null;
        }

        playerRb.linearVelocity = Vector2.zero;

    }

    // 카메라 이동 및 size 변경 코루틴
    IEnumerator MoveCameraCoroutine()
    {
        Vector3 startPosition = mainCamera.transform.position;
        float startSize = mainCamera.orthographicSize;
        float elapsedTime = 0f;

        while (elapsedTime < cameraMoveDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetCameraPosition, elapsedTime / cameraMoveDuration);
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetCameraSize, elapsedTime / cameraMoveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종적으로 정확한 목표 위치와 사이즈로 설정
        mainCamera.transform.position = targetCameraPosition;
        mainCamera.orthographicSize = targetCameraSize;

    }

    IEnumerator FoxMoveCoroutine()
    {
        while (fox.transform.position.x < foxPosition.x)
        {
            Vector2 direction = (foxPosition - (Vector2)transform.position).normalized;
            foxRb.linearVelocity = direction * foxSpeed;
            yield return null;
        }

        foxRb.linearVelocity = Vector2.zero;
        Destroy(fox);

    }

}