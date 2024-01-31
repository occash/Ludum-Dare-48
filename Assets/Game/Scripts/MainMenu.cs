using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject transitionScreenPrefab;
    
    private TransitionManager transitionManager;

    public void StartGame()
    {
        SceneManager.LoadScene(1);

        /*transitionManager.Hide().onComplete += () =>
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(1);
            operation.completed += (op) => transitionManager.Show();
        };*/
    }

    public void Exit()
    {
        Application.Quit();
    }

    /*private void Awake()
    {
        GameObject transitionScreenObject = GameObject.FindGameObjectWithTag("Transition");

        if (transitionScreenObject == null)
            transitionScreenObject = Instantiate(transitionScreenPrefab);


        transitionManager = transitionScreenObject.GetComponent<TransitionManager>();
    }*/
}
