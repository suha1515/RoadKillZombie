using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStageInfo : MonoBehaviour
{
    public bool OnActive = false;
    public float speed = 1.0f;
    [SerializeField] Vector2 hidePosition = new Vector2();
    [SerializeField] Vector2 ActivePosition = new Vector2();
    public string stageName = null;
    [SerializeField] UILabel stageNameLabel = null; 

    // Use this for initialization
    void Start()
    {
        ActivePosition = Vector2.zero;
        hidePosition = new Vector2(950.0f, 0.0f);
        transform.localPosition = hidePosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateActive();
    }

    void UpdateActive()
    {
        if (OnActive)
        {
            transform.localPosition = Vector2.Lerp(transform.localPosition, ActivePosition, Time.deltaTime * speed);
            stageNameLabel.text = stageName;

        }
        else
        {
            transform.localPosition = Vector2.Lerp(transform.localPosition, hidePosition, Time.deltaTime * speed);
        }
    }
}
