using UnityEngine;


public class InGameText : MonoBehaviour
{

    [HideInInspector] public string PointAmount;
    [SerializeField] private TextMesh textMesh;

    private float disappearTime = 1f;
    private Color textColor;

    private void Start()
    {
        textMesh = transform.GetComponent<TextMesh>();
        textMesh.text = PointAmount;
        textColor = textMesh.color;
    }

    private void Update()
    {
        float moveYSpeed = 1;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTime -= Time.deltaTime;

        if(disappearTime < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if(textColor.a < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
