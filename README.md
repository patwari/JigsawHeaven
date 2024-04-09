# Jigsaw Heaven

This is an example Jigsaw Game made in Unity using masking to create tiles. Of course, it highly un-optimized. But what more can be expected from a game which was created in just few hours.

## Features

- Uses masking to create tiles.
- Contains a central DI to hold and pass utility class objects.
- Saves and restores the game progress. You may quit the app, while a puzzle is running, and may resume it later.
- Store progress in files. So that it can be synchronized over cloud, if ever we want to enable cloud features.
- Use Observer pattern, via EventsModels that sends signals.
- Almost all modules are decoupled from one another. Lobby has no knowledge of Game, and vice-versa.
- A very generic SoundManager is provided. Though, currently it plays only a single sound.
- Various Utils are added as well. For example:
  - FileUtils = which contains stateless file read/write methods.
  - SingletonGameObjects = which enables items to be kept in memory, while ensuring that only 1 instance exists. A bit better than using Singleton pattern.
  - SoundableButton = Just add this script to a button, and it add a sound when clicked. Useful :-)

## Future Improvements

- We can use `Bézier curve` to create tiles.
- We can also pre-bake (in runtime) the textures of each tile.
- Currently Lobby has no stored progress. We can easily save it's progress in a separate file as well.
- It contains NO animations. :-/
