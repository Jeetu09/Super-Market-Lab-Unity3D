
using UnityEngine;

public class IndoorMat : MonoBehaviour
{
   void OnCollisionEnter(Collision collinfo)
    {
        if(collinfo.collider.name == "Mat")
        {
            Debug.Log("Yes We Hit on each other");
        }
    }
}
