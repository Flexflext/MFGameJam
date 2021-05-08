using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreAddBehavior : MonoBehaviour
{
    public float ScoreToAdd;

    [SerializeField] private TMP_Text score;

    private void Start()
    {
        score.text = "+" + ScoreToAdd.ToString();
    }

    public void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }
}
