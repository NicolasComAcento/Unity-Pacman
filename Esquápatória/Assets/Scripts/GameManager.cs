using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dog[] dogs;
    public Duck duck;
    public Transform pellets;

    public Text gameOverText;
    public Text scoreText;
    public Text livesText;

    public int dogsMultiplier { get; private set; } = -1;

    public int score { get; private set; }
    public int lives { get; private set; }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }
    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewRound()
    {
        gameOverText.enabled = false;

        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }

    private void ResetState()
    {
        ResetDogsMultiplier();
        for (int i = 0; i < this.dogs.Length; i++)
        {
            this.dogs[i].ResetState();
        }
        this.duck.ResetState();
    }
    private void GameOver()
    {

        gameOverText.enabled = true;

        for (int i = 0; i < this.dogs.Length; i++)
        {
            this.dogs[i].gameObject.SetActive(false);
        }
        this.duck.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }

    public void DogKnockout(Dog dog)
    {
        int points = dog.points * this.dogsMultiplier;
        SetScore(this.score + points);
        this.dogsMultiplier++;
    }

    public void DuckBited()
    {
        this.duck.gameObject.SetActive(false);

        SetLives(this.lives - 1);
        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 2.0f);
        }
        else
        {
            GameOver();
        }
    }
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);

        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets())
        {
            this.duck.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);

        }
    }
    public void PowerPelletEaten(PowerPellet pellet)
    {
        for( int i = 0; i< this.dogs.Length; i++)
        {
            this.dogs[i].frightened.Enable(pellet.duration);
        }


        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetDogsMultiplier), pellet.duration);
    }
    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
    
     private void ResetDogsMultiplier()
    {
        this.dogsMultiplier = 1;
    }
}

