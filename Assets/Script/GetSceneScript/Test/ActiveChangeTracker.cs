using UnityEngine;

public class ActiveChangeTracker : MonoBehaviour
{
    private void SetActive(bool value)
    {
        if (gameObject.activeSelf != value)
        {
            Debug.Log($"{gameObject.name} is being set to {(value ? "Active" : "Inactive")} by {GetCaller()}");
            gameObject.SetActive(value);
        }
    }

    private string GetCaller()
    {
        var stackTrace = new System.Diagnostics.StackTrace(true);
        var frames = stackTrace.GetFrames();
        if (frames != null && frames.Length > 1)
        {
            var caller = frames[1].GetMethod();
            return $"{caller.DeclaringType.FullName}.{caller.Name}";
        }
        return "Unknown";
    }
}
