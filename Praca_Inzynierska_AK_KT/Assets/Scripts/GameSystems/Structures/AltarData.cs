using UnityEngine;

namespace GameSystems
{
    public class AltarData : MonoBehaviour
    {
		public Vector2 Position { get; private set; }
		public bool Occupied {  get; set; }

        void Start()
        {
			Occupied = false;
			Position = this.gameObject.transform.position;
        }

        void Update()
        {
			
        }
    }
}
