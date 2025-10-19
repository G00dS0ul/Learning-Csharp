# RolePlayingGame

A small, short text-based Role Playing Game written in C# as part of the Learning-Csharp repository.

## Description

This project implements a simple console-based RPG demonstrating object-oriented design, player/enemy interactions, and basic game loops.

## Requirements

- .NET 6.0 SDK or later

## Build & Run

1. Open a terminal and change to the RolePlayingGame directory:
   cd RolePlayingGame
2. Build and run the game:
   dotnet run

## Gameplay

The game is a simple turn-based, text-driven RPG played from the console. Below are the core gameplay elements, commands, and example interactions.

Core elements
- Player: Controls a character with health (HP), attack power, defense, and an inventory (e.g., healing potions).
- Enemies: Appear one at a time; each has HP, attack, and sometimes special behaviors.
- Progression: Players gain XP from defeated enemies and can level up to increase stats (if implemented).
- Turns: Each encounter proceeds in turns. On your turn you choose an action; then the enemy acts.

Player actions / commands
- attack  - Perform a basic attack against the current enemy.
- defend  - Brace to reduce damage taken on the next enemy attack.
- heal    - Use a healing item from your inventory to restore HP (if available).
- status  - Show current player HP, level/XPs (if present), inventory items, and enemy HP.

Example enemy types (suggested)
- Goblin — low HP, low damage, common.
- Orc — medium HP, stronger attack.
- Troll — high HP, deals heavy damage occasionally.
- Boss — unique behavior, high HP, special attacks.

## Contributing

Contributions and suggestions are welcome. Open an issue or submit a pull request.

## License

See the repository license (if any).
