using UnityEngine;

public class FisnishController : MonoBehaviour
{
    [SerializeField] private GameObject finishText;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Roberto"))
        {
            finishText.SetActive(true);
            Time.timeScale = 0;
            print("Has ganado");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
