using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu()]
public class RecipeListSO : ScriptableObject
{
    // Although we could have just whacked this in a list in the delivery manager, that approach would require us to duplicate the list
    // in the case where another class needs to access the same list. By storing the recipes in their own ScriptableObject, we only have
    // to pass a single reference into the delivery manager, and any potential future classes that needs the same data
    public List<RecipeSO> recipeSOList;
}
