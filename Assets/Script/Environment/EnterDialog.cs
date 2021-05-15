using UnityEngine;

public class EnterDialog : MonoBehaviour
{

    // the enter dialog
    public GameObject dialog;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // set the dialog active for pop up the dialog
            dialog.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // set the dialog deactivate for hide the dialog
            dialog.SetActive(false);
        }
    }
}
