using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GameSystem;

namespace SceneObjects
{
    public class DomeController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        public void ChooseDomePartToBroke()
        {
            List<GameObject> unbrokenPieces = GameManager.instance.allDomePieces.Where(x => (!x.Value.isBroken && !x.Value.isRepairing)).Select(x => x.Key).ToList();
            int randomPart = Random.Range(0, unbrokenPieces.Count);
            GameManager.instance.allDomePieces[unbrokenPieces[randomPart]].InitDamage();
        }
    }
}