using UnityEngine;
using UnityEngine.UI;

public class EnemysLeft : MonoBehaviour
{
    public Transform Enemys;
    public Text Enemys_Left;

    // Update is called once per frame
    void Update()
    {
        Enemys_Left.text = Enemys.childCount.ToString();
    }
}
