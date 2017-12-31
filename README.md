# Unity Property Validator

One of the most common and annoying bugs occurring with Unity development are missing 
references in components.

This extension aims to detect these bugs as early as possible to save time and nerves of developers.

The validator assumes that all fields are required to have a value, unless they are explicity marked as optional by the programmer.

## Features
 - Highlight missing references in inspector

## Usage

Copy Assets/PropertyChecker to your project and you're good to go. The extension is enabled when the the text "Property checker enabled." appears in inspector.

All fields are expected to have a value, unless the field has "Optional" attribute.

```C#

public class TestScript : MonoBehaviour {

    // These properties are required to have an assigned value.
	[SerializeField] UnityEngine.Object RequiredField;
    public UnityEngine.Object RequiredField;

    // These properties are not validated by the Property Validator
	[SerializeField, Optional] UnityEngine.Object OptionalField;

	[Optional] 
    public UnityEngine.Object OptionalField;

}
``` 

## Upcoming features

 - 

