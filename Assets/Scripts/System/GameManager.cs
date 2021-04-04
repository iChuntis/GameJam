using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneObjects;
using UserInterface;

namespace GameSystem
{
    public class GameManager : MonoBehaviour
    {
        
        // Здесь мы делаем словари всех значимых объектов сцены, чтобы потом к ним обращаться не используя GetComponent
        // Данный скрипт - синглетон
        public static GameManager instance;

        // ..........................
        // Указатель на объект "игрок"
        //public Dictionary<GameObject, Player> player;
        // Словарь объектов типа "волонтеры"
        //public Dictionary<GameObject, Volunteers> allVolunteers;
        // Словарь объектов типа "кусок купола"
        public Dictionary<GameObject, DomePiece> allDomePieces;
        // Словарь объектов типа "дом"
        public Dictionary<GameObject, HomeObject> allHomes;
        // Словарь объектов типа "лужа"
        //public Dictionary<GameObject, PoolObject> allPools;


        // Число всех участков купола
        [SerializeField]
        int domePieces;
        // Число здоровых участков купола
        int domeUnbrokenPieces;
        // Число населения
        [SerializeField]
        int populationVol;
        // Число волонтеров
        int volunteerVol;
        int accesibleVolunteers;

        [SerializeField]
        int cityMaxLifePoints;

        int cityLifePoints;
        int nonSavedPeople;

        int gameDurationSeconds;
        float gameDurationSecs;

        public int DomePieces
        {
            get{return domePieces;}
            set{domePieces = value;}
        }
        public int DomeUnbrokenPieces
        {
            get{return domeUnbrokenPieces;}
            set{domeUnbrokenPieces = value;}
        }

        public int DomeBrokenPieces()
        {
            //domePieces = allDomePieces.Count;
            return domePieces-domeUnbrokenPieces;
        }

        public int PopulationVol
        {
            get{return populationVol;}
            set{populationVol = value;}
        }

        public int NonSavedPeople
        {
            get{return nonSavedPeople;}
            set{nonSavedPeople = value;}
        }
        public int VolunteerVol
        {
            get{return volunteerVol;}
            set{volunteerVol = value;}
        }

        
        // Start is called before the first frame update
        void Awake()
        {
            if (instance == null)
			{
				instance = this;
			}
			else
			{
				Destroy(this.gameObject);
			}

            //player = new Dictionary<GameObject, Player>();
            //allVolunteers = new Dictionary<GameObject, Volunteers>();
            allHomes = new Dictionary<GameObject, HomeObject>();
            //allPools = new Dictionary<GameObject, PoolObject>();
            allDomePieces = new Dictionary<GameObject, DomePiece>();

            domePieces = 23;
            domeUnbrokenPieces = domePieces;
            volunteerVol = 0;
            populationVol = 6;
            nonSavedPeople = 0;

            cityLifePoints = cityMaxLifePoints;
            cityLifePoints = 100;
            gameDurationSeconds = 0;
            gameDurationSecs = 0.0f;
        }

        public void Start()
        {
            UIController.instance.SetPopulationNum(populationVol);
            UIController.instance.SetNonSavedNum(nonSavedPeople);
        }

        void Update()
        {
            gameDurationSecs += Time.deltaTime;
            if (gameDurationSecs >= 1.0f)
            {
                gameDurationSecs = 0.0f;
                gameDurationSeconds += 1;
                UIController.instance.SetNewTimeResult(gameDurationSeconds);
            }
        }

        
        public void DomePartChanged(bool broken)
        {
            if (broken) // under broke
            {
                domeUnbrokenPieces -= 1;
            }
            else // repaired
            {
                domeUnbrokenPieces += 1;
                domeUnbrokenPieces = Mathf.Max(domeUnbrokenPieces, domePieces);
            }
        }

        /*
        public void PeopleSavedChange()
        {
            
        }
        */

        public void ChangePeople(int val)
        {
            populationVol += val;

            UIController.instance.SetPopulationNum(populationVol);
            UIController.instance.SetNonSavedNum(nonSavedPeople);
        }

        public void ChangeVolunteers(int val)
        {
            volunteerVol += val;
        }

        public void ChangeNotSavedPeople(int val)
        {
            nonSavedPeople += val;
            nonSavedPeople = Mathf.Max(0, nonSavedPeople);
            UIController.instance.SetNonSavedNum(nonSavedPeople);
            Debug.Log("Add " + val + " non-saved people. Here are " + nonSavedPeople + " nonSavedPeople");
        }

        public void ChangeCityLifePoints(int points)
        {
            cityLifePoints += points;
            cityLifePoints = Mathf.Min(cityLifePoints, cityMaxLifePoints);

            UIController.instance.SetDomeBalance(((float)cityLifePoints / (float)cityMaxLifePoints));

            if (cityLifePoints <= 0)
            {
                // Город проиграл
                UIController.instance.ShowEndText();
                StartCoroutine(FinishGame());
            }
        }

        IEnumerator FinishGame()
        {
            yield return new WaitForSeconds(1.5f);
            UltimateFinishGame();
        }

        void UltimateFinishGame()
        {
            UIController.instance.OnMenuButtonClick();
            Messenger<float>.Broadcast("MatchFinished", Time.time);
        }
    }
}