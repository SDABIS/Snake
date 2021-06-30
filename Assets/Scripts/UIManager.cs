using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        EventBroker.Instance.OnGameOver.AddListener(HandleGameOver);
    }

    private void HandleGameOver() {
        Debug.Log("AAAA");
        UI.SetActive(true);
    }

    public void OnRestartClick() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
