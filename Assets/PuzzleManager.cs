using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    private SnapZone[] snapZones;

    private void Awake()
    {
        Instance = this;
        snapZones = FindObjectsOfType<SnapZone>();
    }

    public void CheckCompletion()
    {
        foreach (var zone in snapZones)
        {
            if (!zone.isSnapped)
                return;
        }

        OnPuzzleComplete();
    }

    private void OnPuzzleComplete()
    {
        Debug.Log("✅ Puzzle Completed!");

        // Example: show a final combined object or celebration
        // You can enable an object, play audio, etc.
        // e.g. finalModel.SetActive(true);
    }
}
