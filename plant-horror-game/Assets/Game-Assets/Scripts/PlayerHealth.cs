using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth;

    public float coolDownTime = 2.0f; //no instant death from colliding with fire
    private bool inCoolDown = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curHealth = maxHealth; // start from full health
    }

    // Update is called once per frame
    void Update()
    {

        if (inCoolDown)
        {
            coolDownTime -= Time.deltaTime;
            if (coolDownTime <= 0)
            {
                inCoolDown = false;
                coolDownTime = 2.0f; // reset cooldown timer
            }
        }
        if (curHealth == 0)
        {
            Debug.Log("Player has died.");
            SceneManager.LoadScene("RestartGame");
        }
        
    }

    public void TakeFireDamage(int damage)
    {
        if(inCoolDown == false)
        {
            curHealth -= damage;
            inCoolDown = true;
            Debug.Log(gameObject.name + " took " + damage + " damage. Current Health: " + curHealth);

            if (curHealth <= 0)
            {
                // Handle player death TBD
                Debug.Log(gameObject.name + " has died.");
                SceneManager.LoadScene("RestartGame");
                SceneManager.UnloadSceneAsync("Player");
                //SceneManager.UnloadSceneAsync("Lab-Scene");

             //   Destroy(gameObject);
            }
        }
    }
}
