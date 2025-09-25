# GUID ID Relationship Management Solution

## Problem Statement
The original issue was that entities with GUID IDs generated at runtime couldn't be properly referenced in JSON files or external configurations, while still maintaining proper relationships between entities.

## Issues Fixed

### 1. Hard-coded Schedule IDs
**Before:** Schedules had hard-coded integer IDs (1, 2, 3, 4) that could conflict with database-generated values.
**After:** Schedules use `Id = 0` to let Entity Framework generate appropriate IDs.

### 2. Duplicate Movie IDs
**Before:** Both movies in the factory shared the same `movieId`.
**After:** Each movie has its own unique GUID ID.

### 3. Inconsistent Genre Relationships
**Before:** Genres had completely random GUIDs that couldn't be referenced externally.
**After:** Common genres use well-known GUIDs that can be referenced in JSON files.

## Solution Approach

### Well-Known Genre IDs
```csharp
public static readonly Guid ActionGenreId = Guid.Parse("11111111-1111-1111-1111-111111111111");
public static readonly Guid ComedyGenreId = Guid.Parse("22222222-2222-2222-2222-222222222222");
public static readonly Guid SadGenreId = Guid.Parse("33333333-3333-3333-3333-333333333333");
```

### Benefits
1. **JSON Serialization Safe**: Entities can be serialized to JSON and deserialized while maintaining relationships
2. **Consistent IDs**: Same genre names always get the same GUID across different runs
3. **No Hard-coded Conflicts**: No more hard-coded integer IDs that conflict with database generation
4. **Extensible**: New genres still get unique GUIDs while common ones are predictable

### Example Usage in JSON
```json
{
  "id": "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
  "name": "Movie 1",
  "genres": [
    {
      "id": "11111111-1111-1111-1111-111111111111",
      "name": "Action"  
    },
    {
      "id": "22222222-2222-2222-2222-222222222222",
      "name": "Comedy"
    }
  ]
}
```

## Tests Added
- `MovieFactory_ShouldCreateConsistentGenreIds_ForSameGenreNames`: Verifies same genre names get same IDs
- `MovieFactory_ShouldCreateUniqueMovieIds`: Ensures each movie has unique ID
- `MovieWithGenres_ShouldSerializeAndDeserializeWithConsistentIds`: Tests JSON serialization roundtrip
- `GenreIdConstants_ShouldBeWellKnownValues`: Validates the well-known GUID constants

This solution allows the system to work with JSON configurations while maintaining proper relational integrity.