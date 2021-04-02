using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneObjects;

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
        public int PopulationVol
        {
            get{return populationVol;}
            set{populationVol = value;}
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

            domeUnbrokenPieces = domePieces;
            volunteerVol = 0;
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

        public void PeopleSavedChange()
        {
            
        }
        
    }
}