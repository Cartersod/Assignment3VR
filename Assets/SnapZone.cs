using UnityEngine;
using System.Collections;

public class SnapZone : MonoBehaviour
{
    public int snapID;              // Example: 1 for Snap1, 2 for Snap2, etc.
    public Transform snapPoint;     // The position to align the piece to
    public bool isSnapped = false;

    [Header("Snap Settings")]
    public float snapSpeed = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (isSnapped) return;

        // Check if this is the matching piece
        if (other.name == "Piece" + snapID)
        {
            // Disable physics and grabbing
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            var grab = other.GetComponent<OVRGrabbable>();
            if (grab) grab.enabled = false;

            // Start smooth snapping
            StartCoroutine(SnapPiece(other.transform));

            isSnapped = true;
            PuzzleManager.Instance.CheckCompletion();
        }
    }

    private IEnumerator SnapPiece(Transform piece)
    {
        Vector3 startPos = piece.position;
        Quaternion startRot = piece.rotation;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * snapSpeed;
            piece.position = Vector3.Lerp(startPos, snapPoint.position, t);
            piece.rotation = Quaternion.Slerp(startRot, snapPoint.rotation, t);
            yield return null;
        }

        piece.position = snapPoint.position;
        piece.rotation = snapPoint.rotation;
        piece.SetParent(snapPoint);
    }
}
