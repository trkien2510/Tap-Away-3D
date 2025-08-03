using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MoveBlock : Subject
{
    private Vector3 initialPosition;
    private float moveSpeed = 5f;

    [HideInInspector]
    public bool isMoving = false;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void OnEnable()
    {
        transform.position = transform.parent.TransformPoint(initialPosition);
    }

    private void Update()
    {
        if (isMoving)
        {
            Move();
        }
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance != null &&
            !GameManager.Instance.IsProcessing &&
            !GetComponent<BlockProperties>().isHardBlock &&
            !GetComponent<BlockProperties>().isBlockCounting)
        {
            isMoving = true;
            GameManager.Instance.IsProcessing = true;
            NotifyObserver(GameEvent.MinusMove);
            NotifyObserver(GameEvent.MoveSFX);
        }

        if (!IsPathBlocked())
        {
            NotifyObserver(GameEvent.MinusCounting);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isMoving)
        {
            isMoving = false;
            GameManager.Instance.IsProcessing = false;
            StartCoroutine(FlashColor(Color.red, 0.25f));
        }

        NotifyObserver(GameEvent.BlockedSFX);

        Vector3 localPos = transform.parent.InverseTransformPoint(transform.position);
        localPos = new Vector3(Mathf.Round(localPos.x), Mathf.Round(localPos.y), Mathf.Round(localPos.z));
        transform.position = transform.parent.TransformPoint(localPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        isMoving = false;
        GameManager.Instance.IsProcessing = false;

        transform.position = Vector3.one * 1000f;
        gameObject.SetActive(false);
    }

    private void Move()
    {
        transform.position += -transform.right * moveSpeed * Time.deltaTime;
    }

    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, -transform.right);
        float maxDistance = 1.1f;

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            var otherBlock = hit.collider.GetComponent<BlockProperties>();
            if (otherBlock != null && otherBlock != this)
            {
                return true;
            }
        }

        return false;
    }


    private IEnumerator FlashColor(Color color, float duration)
    {
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Plane"))
            {
                var renderer = child.GetComponent<Renderer>();
                if (renderer != null) renderer.material.color = color;
            }
        }

        yield return new WaitForSeconds(duration);

        foreach (Transform child in transform)
        {
            if (child.name.Contains("Plane"))
            {
                var renderer = child.GetComponent<Renderer>();
                if (renderer != null) renderer.material.color = Color.white;
            }
        }
    }
}
