using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lighthouse{
    public class Turret_Controller : MonoBehaviour
    {
		public static Turret_Controller instance;
		public void Awake()
		{
			instance = this;
		}
	}
}

