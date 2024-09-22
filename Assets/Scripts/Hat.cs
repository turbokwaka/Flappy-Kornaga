using UnityEngine;

public class Hat : MonoBehaviour
{
    public HatCollection hatCollection;
    public Transform parentTransform;
    
    private void Start()
    {
        Instantiate(hatCollection.EquippedItem().prefab, parentTransform);
    }
}

