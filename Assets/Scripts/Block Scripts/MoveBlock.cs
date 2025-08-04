using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MoveBlock : Subject
{
    private float moveSpeed = 10f;

    [HideInInspector] public bool isMoving = false;
    private Vector3 mouseDownPos;

    private void OnEnable()
    {
        ChangeMaterialColor("Plane", Color.white);
        FindObjectOfType<AudioEventListener>().RegisterObservers();
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
        mouseDownPos = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        float distance = Vector3.Distance(mouseDownPos, Input.mousePosition);
        if (distance < 0.1f)
        {
            if (GameManager.Instance != null &&
            !GameManager.Instance.IsProcessing &&
            !GetComponent<BlockProperties>().isHardBlock &&
            !GetComponent<BlockProperties>().isBlockCounting)
            {
                isMoving = true;
                GameManager.Instance.IsProcessing = true;
                ChangeMaterialColor("Plane", Color.green);
                NotifyObserver(GameEvent.MinusMove);
                NotifyObserver(GameEvent.MoveSFX);

                if (!IsPathBlocked())
                {
                    NotifyObserver(GameEvent.MinusCounting);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isMoving)
        {
            isMoving = false;
            if (GameManager.Instance.TotalMoves > 0)
            {
                GameManager.Instance.IsProcessing = false;
            }
            StartCoroutine(FlashColor());
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
        Vector3 direction = -transform.right;
        float maxDistance = 10f;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, maxDistance))
        {
            if (hit.collider.gameObject != gameObject &&
                hit.collider.GetComponent<BlockProperties>() != null)
            {
                return true;
            }
        }

        return false;
    }

    private IEnumerator FlashColor()
    {
        ChangeMaterialColor("Plane", Color.red);
        yield return new WaitForSeconds(0.25f);
        ChangeMaterialColor("Plane", Color.white);
    }

    private void ChangeMaterialColor(string childName, Color color)
    {
        foreach (Transform child in transform)
        {
            if (child.name.Contains(childName))
            {
                var renderer = child.GetComponent<Renderer>();
                if (renderer != null) renderer.material.color = color;
            }
        }
    }
}
