using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] private string actorName;

    public string ActorName => actorName;
}
