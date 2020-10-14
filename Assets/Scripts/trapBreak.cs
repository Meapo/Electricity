using UnityEngine;
public class trapBreak : MonoBehaviour
{
    

    public void BreakTrap()
    {
        gameObject.GetComponent<Crasher>().Crash();
        gameObject.SetActive(false);
    }

}
