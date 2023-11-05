
using UnityEngine;

public interface ICollectable 
{
    void Collect();


}

public enum Modifier { }

public class Collectible : MonoBehaviour
{
    private Modifier type;

    public Modifier GetCollectibleType() => type;

    private void OnTriggerEnter(Collider other)
    {
        
    }

}