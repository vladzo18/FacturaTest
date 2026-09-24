# FacturaTest

Unity/C# game project focused on gameplay systems and software architecture.

## Overview

A prototype mobile game built with Unity and C#. The project was used to experiment with modular gameplay architecture, dependency injection and reusable game systems.

## Features

* Vehicle gameplay
* Enemy spawning and behaviour
* Enemy state machine
* Gameplay states
* Mobile input
* Camera systems
* Health system
* UI systems
* Object pooling
* Asset loading abstraction
* Game rules
* Particle spawning

## Architecture

The project separates gameplay functionality into independent systems.

```text
Gameplay
├── Car
├── Enemies
├── GameRules
├── GameplayStates
├── Health
├── Services
│   ├── InputService
│   └── StoperService
├── UI
└── Root
    └── StateMachine
```

The project also uses dependency injection through **Zenject**.

Example:

```csharp
Container
    .Bind<IInputService>()
    .To<MobileInput>()
    .AsSingle();
```

Other architectural techniques used in the project:

* Dependency Injection
* Interfaces and abstractions
* State Machine
* Factory pattern
* Object Pooling
* Repository-like service abstractions
* Event-driven communication

## Tech Stack

* Unity
* C#
* Zenject
* DOTween
* Unity UI
* Unity Physics
* Git

## Project Status

Prototype / educational project.

## Author

**Vladyslav Osmak**

[GitHub](https://github.com/vladzo18)
