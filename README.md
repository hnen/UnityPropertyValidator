# Unity Property Validator

One of the most common and annoying bugs occurring with Unity development are missing 
references in components.

This extension aims to detect these bugs as early as possible to save time and nerves of developers.

The validator assumes that all fields are required to have a value, unless they are explicity marked as optional by the developer.

The extension, while probably usable, is still WIP and not tested in practice.

## Features
 - Highlight missing references in inspector

 ![Screenshot of object inspector with validator enabled](./doc/readme_img.png)

## Known bugs/issues
 - Not tested for arrays and serializable structs.
 - As the extension relies on overriding the default inspector, this may easily conflict with other extension that may do the same.
 - The extension is now disabled for prefabs, as they may have intentionally blank references since they may be meant to be assigned after adding it to the scene.
 

## Usage

Copy Assets/PropertyChecker to your project and you're good to go. The extension is enabled when the the text "Property checker enabled." appears in inspector.

All fields are expected to have a value, unless the field has "Optional" attribute.

```C#
using UnityEngine;
using PropertyChecker;

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
 - Create NUnit tests for missing values.
 - Reorganize folder structure: separate example project from the extension. Maintain a DLL from extension.
 - Scan the scene for missing references on load, keep track of them when scene is edited.
 - Emit errors from missing references.
 - Prevent starting the game if there are unassigned fields.
 - Support for prefabs? Would require explicit attribute for fields that are meant to be assigned on a scene so the validator wouldn't complain missing references on those properties.
 
