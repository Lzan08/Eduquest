using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Soal
{
    public string stringSoal;
    public Sprite spriteSoal;
    public string[] stringJawaban;
    public Sprite[] spriteJawaban;
    public int kunciJawaban;
    public GameObject pemilikSoal; 
}

public class SoalManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public Soal[] soals;
    public int indexSoalEnemy;
    public int[] indexRandomJawaban;

    [Header("UI jawaban dan Soal")]
    public TMP_Text soalText;
    public Image soalImage;
    public Button[] buttons;
    public Image[] imageButtons;

    private int currentEnemyIndex;
    private int currentQuestionIndex;
    private Soal[] enemySoals;

    private List<Soal> usedSoals = new List<Soal>();

    public event Action OnQuestionsComplete;

    private AudioManager audioManager;
    public TMP_Text NamePlayer;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        GenerateRandomJawabanIndices();
        NamePlayer.text = PlayfabManager.stringNamePlayer;
    }

    void GenerateRandomJawabanIndices()
    {
        indexRandomJawaban = Enumerable.Range(0, 4).OrderBy(x => UnityEngine.Random.value).ToArray();
    }

    public void SetEnemyQuestions(GameObject karakterDitabrak)
    {
        enemySoals = soals.Where(soal => soal.pemilikSoal == karakterDitabrak).ToArray();
        
        if (enemySoals.Length == 0)
        {
            
            return;
        }

        currentQuestionIndex = 0;
        SetSoal(enemySoals[currentQuestionIndex]);
    }

    private Soal[] GetUniqueRandomSoals(int count)
    {
        List<Soal> availableSoals = soals.Except(usedSoals).ToList();
        Soal[] selectedSoals = availableSoals.OrderBy(x => UnityEngine.Random.value).Take(count).ToArray();
        usedSoals.AddRange(selectedSoals);
        return selectedSoals;
    }

    public void SetSoal(Soal soal)
    {
        soalText.text = soal.stringSoal;
        if (soal.spriteSoal != null)
        {
            soalImage.sprite = soal.spriteSoal;
            soalImage.gameObject.SetActive(true);
        }
        else
        {
            soalImage.gameObject.SetActive(false);
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            int answerIndex = indexRandomJawaban[i];
            if (answerIndex < soal.spriteJawaban.Length && soal.spriteJawaban[answerIndex] != null)
            {
                imageButtons[i].sprite = soal.spriteJawaban[answerIndex];
                imageButtons[i].gameObject.SetActive(true);
                buttons[i].gameObject.SetActive(false);
            }
            else if (answerIndex < soal.stringJawaban.Length)
            {
                buttons[i].GetComponentInChildren<Text>().text = soal.stringJawaban[answerIndex];
                buttons[i].gameObject.SetActive(true);
                imageButtons[i].gameObject.SetActive(false);
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
                imageButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void NextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex < enemySoals.Length)
        {
            SetSoal(enemySoals[currentQuestionIndex]);
        }
        else
        {
            
            OnQuestionsComplete?.Invoke();
        }
    }

    public void ButtonJawaban(int indexJawaban)
    {
        Soal currentSoal = enemySoals[currentQuestionIndex];
        if (indexRandomJawaban[indexJawaban] == currentSoal.kunciJawaban)
        {
            audioManager.PlaySFX(audioManager.Benar);
            scoreManager.IncreaseScore();
            NextQuestion();
        }
        else
        {
            audioManager.PlaySFX(audioManager.Salah);
            scoreManager.DecreaseScore();
        }
    }
}