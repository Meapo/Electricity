using UnityEngine;
public class trapBreak : MonoBehaviour
{
    public float delayTime;

    public void BreakTrap()
    {
        // break anim

        Destroy(gameObject, delayTime);
    }

}
