using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PropertyChecker;

public class TestScript : MonoBehaviour {

	[SerializeField] UnityEngine.Object RequiredField;
	[SerializeField, Optional] UnityEngine.Object OptionalField;

}
